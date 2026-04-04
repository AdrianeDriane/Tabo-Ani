using Microsoft.EntityFrameworkCore;
using TaboAni.Api.ExceptionHandling;
using TaboAni.Api.Application.Configuration;
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

DotEnv.Load(Path.Combine(Directory.GetCurrentDirectory(), ".env"));

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "Missing ConnectionStrings:DefaultConnection. Set it in server/TaboAni.Api/.env.");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiDocumentation();
builder.Services.AddApplicationDependencies();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

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
app.UseCors("Frontend");
app.MapControllers();

app.Run();
