using System.Security.Claims;
using System.Threading.RateLimiting;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using TaboAni.Api.ExceptionHandling;
using TaboAni.Api.Application.Configuration;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Extensions;
using TaboAni.Api.Data;
using TaboAni.Api.Data.Seeding;
using TaboAni.Api.Verification;

if (args.Contains("--verify-schema", StringComparer.Ordinal))
{
    Environment.ExitCode = SchemaVerificationRunner.Run();
    return;
}

var shouldSeedFarmerData = args.Contains("--seed-farmer-data", StringComparer.Ordinal);
var shouldSeedPlatformRoles = args.Contains("--seed-platform-roles", StringComparer.Ordinal);

DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

var builder = WebApplication.CreateBuilder(args);
var frontendOptions = builder.Configuration.GetSection(FrontendOptions.SectionName).Get<FrontendOptions>() ?? new FrontendOptions();
var authOptions = builder.Configuration.GetSection(AuthOptions.SectionName).Get<AuthOptions>() ?? new AuthOptions();
var authRateLimitOptions = builder.Configuration.GetSection(AuthRateLimitOptions.SectionName).Get<AuthRateLimitOptions>() ?? new AuthRateLimitOptions();
var effectiveSigningKey = string.IsNullOrWhiteSpace(authOptions.SigningKey)
    ? new string('x', 32)
    : authOptions.SigningKey.Trim();

if (!shouldSeedFarmerData && !shouldSeedPlatformRoles)
{
    ValidateAuthOptions(authOptions);
}

var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Missing ConnectionStrings:DefaultConnection. Set it in server/TaboAni.Api/.env.");

var connectionStringBuilder = new NpgsqlConnectionStringBuilder(rawConnectionString);
if (connectionStringBuilder.Pooling &&
    !string.IsNullOrWhiteSpace(connectionStringBuilder.Host) &&
    connectionStringBuilder.Host.EndsWith(".pooler.supabase.com", StringComparison.OrdinalIgnoreCase))
{
    // Supabase already fronts the database with its own pooler. Disabling the
    // client-side Npgsql pool avoids a driver/pooler reuse bug seen on
    // back-to-back commands against the pooler endpoint.
    connectionStringBuilder.Pooling = false;
}

var connectionString = connectionStringBuilder.ConnectionString;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiDocumentation();
builder.Services.AddApplicationDependencies();
builder.Services.AddHttpContextAccessor();
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(AuthOptions.SectionName));
builder.Services.Configure<FrontendOptions>(builder.Configuration.GetSection(FrontendOptions.SectionName));
builder.Services.Configure<EmailVerificationOptions>(builder.Configuration.GetSection(EmailVerificationOptions.SectionName));
builder.Services.Configure<SignupPolicyOptions>(builder.Configuration.GetSection(SignupPolicyOptions.SectionName));
builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(SmtpOptions.SectionName));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = authOptions.UseSecureRefreshTokenCookie;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = authOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(effectiveSigningKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new ErrorResponseDto
                {
                    Success = false,
                    Message = "Authentication failed.",
                    Errors = ["auth.unauthorized: Access token is missing, invalid, or expired."]
                });
            },
            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new ErrorResponseDto
                {
                    Success = false,
                    Message = "You do not have access to the requested resource.",
                    Errors = ["auth.forbidden: The authenticated user is not allowed to access this resource."]
                });
            }
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(AuthPolicyNames.Admin, policy =>
        policy.RequireAuthenticatedUser().RequireRole(RoleCodes.Admin));
    options.AddPolicy(AuthPolicyNames.Farmer, policy =>
        policy.RequireAuthenticatedUser().RequireRole(RoleCodes.Farmer));
    options.AddPolicy(AuthPolicyNames.Buyer, policy =>
        policy.RequireAuthenticatedUser().RequireRole(RoleCodes.Buyer));
    options.AddPolicy(AuthPolicyNames.Distributor, policy =>
        policy.RequireAuthenticatedUser().RequireRole(RoleCodes.Distributor));
});
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.ContentType = "application/json";

        await context.HttpContext.Response.WriteAsJsonAsync(new ErrorResponseDto
        {
            Success = false,
            Message = "Too many requests.",
            Errors = ["Please wait a moment before trying again."]
        }, cancellationToken);
    };

    options.AddPolicy("auth-signup", httpContext =>
        CreateAuthRateLimitPartition(
            httpContext,
            "signup",
            authRateLimitOptions.SignupPermitLimit,
            TimeSpan.FromMinutes(authRateLimitOptions.SignupWindowMinutes),
            authRateLimitOptions.QueueLimit));

    options.AddPolicy("auth-login", httpContext =>
        CreateAuthRateLimitPartition(
            httpContext,
            "login",
            authRateLimitOptions.LoginPermitLimit,
            TimeSpan.FromMinutes(authRateLimitOptions.LoginWindowMinutes),
            authRateLimitOptions.QueueLimit));

    options.AddPolicy("auth-refresh", httpContext =>
        CreateAuthRateLimitPartition(
            httpContext,
            "refresh",
            authRateLimitOptions.RefreshPermitLimit,
            TimeSpan.FromMinutes(authRateLimitOptions.RefreshWindowMinutes),
            authRateLimitOptions.QueueLimit));

    options.AddPolicy("auth-resend-verification", httpContext =>
        CreateAuthRateLimitPartition(
            httpContext,
            "resend",
            authRateLimitOptions.ResendPermitLimit,
            TimeSpan.FromMinutes(authRateLimitOptions.ResendWindowMinutes),
            authRateLimitOptions.QueueLimit));

    options.AddPolicy("auth-verify-email", httpContext =>
        CreateAuthRateLimitPartition(
            httpContext,
            "verify",
            authRateLimitOptions.VerifyPermitLimit,
            TimeSpan.FromMinutes(authRateLimitOptions.VerifyWindowMinutes),
            authRateLimitOptions.QueueLimit));
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(ResolveAllowedOrigins(frontendOptions))
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

if (shouldSeedPlatformRoles)
{
    // Short-circuit the host for one-off platform role seeding.
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await RoleSeeder.SeedAsync(dbContext);
    return;
}

if (shouldSeedFarmerData)
{
    // Short-circuit the host for one-off local data seeding.
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await FarmerDevelopmentSeeder.SeedAsync(dbContext);
    return;
}

if (app.Environment.IsDevelopment())
{
    app.MapApiDocumentation();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseCors("Frontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

static string[] ResolveAllowedOrigins(FrontendOptions frontendOptions)
{
    var allowedOrigins = frontendOptions.AllowedOrigins
        .Where(origin => !string.IsNullOrWhiteSpace(origin))
        .Select(origin => origin.TrimEnd('/'))
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToArray();

    if (allowedOrigins.Length > 0)
    {
        return allowedOrigins;
    }

    return [frontendOptions.ClientAppBaseUrl.TrimEnd('/')];
}

static RateLimitPartition<string> CreateAuthRateLimitPartition(
    HttpContext httpContext,
    string partitionName,
    int permitLimit,
    TimeSpan window,
    int queueLimit)
{
    var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

    return RateLimitPartition.GetFixedWindowLimiter(
        $"{partitionName}:{remoteIp}",
        _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = permitLimit,
            Window = window,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = queueLimit,
            AutoReplenishment = true
        });
}

static void ValidateAuthOptions(AuthOptions authOptions)
{
    if (string.IsNullOrWhiteSpace(authOptions.SigningKey) || authOptions.SigningKey.Trim().Length < 32)
    {
        throw new InvalidOperationException(
            "Missing Auth:SigningKey. Set a strong secret with at least 32 characters in server/TaboAni.Api/.env.");
    }

    if (authOptions.AccessTokenLifetimeMinutes <= 0)
    {
        throw new InvalidOperationException("Auth:AccessTokenLifetimeMinutes must be greater than zero.");
    }

    if (authOptions.RefreshTokenLifetimeDays <= 0)
    {
        throw new InvalidOperationException("Auth:RefreshTokenLifetimeDays must be greater than zero.");
    }
}

public partial class Program
{
}
