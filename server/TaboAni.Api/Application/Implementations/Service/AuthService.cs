using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using TaboAni.Api.Application.Common;
using TaboAni.Api.Application.Configuration;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Security;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Application.Validation.Auth;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Application.Implementations.Service;

public sealed class AuthService(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    IEmailVerificationNotifier emailVerificationNotifier,
    IOptions<AuthOptions> authOptions,
    IOptions<EmailVerificationOptions> emailVerificationOptions,
    IOptions<FrontendOptions> frontendOptions,
    IOptions<SignupPolicyOptions> signupPolicyOptions) : IAuthService
{
    private const string BuyerRoleCode = "BUYER";
    private const string FarmerRoleCode = "FARMER";
    private const string PendingEmailVerificationAccountStatus = "PENDING_EMAIL_VERIFICATION";
    private const string ActiveAccountStatus = "ACTIVE";
    private const string PendingEmailVerificationStatus = "PENDING_EMAIL_VERIFICATION";
    private const string PendingReviewStatus = "PENDING_REVIEW";
    private const string TermsPolicyType = "TERMS_OF_SERVICE";
    private const string PrivacyPolicyType = "PRIVACY_POLICY";
    private const string DispatchAcceptedStatus = "ACCEPTED";
    private const string VerifiedStatus = "VERIFIED";
    private const string AlreadyVerifiedStatus = "ALREADY_VERIFIED";
    private const string InvalidOrExpiredStatus = "INVALID_OR_EXPIRED";

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IEmailVerificationNotifier _emailVerificationNotifier = emailVerificationNotifier;
    private readonly AuthOptions _authOptions = authOptions.Value;
    private readonly EmailVerificationOptions _emailVerificationOptions = emailVerificationOptions.Value;
    private readonly FrontendOptions _frontendOptions = frontendOptions.Value;
    private readonly SignupPolicyOptions _signupPolicyOptions = signupPolicyOptions.Value;

    public async Task<AuthSessionResult> LoginAsync(
        LoginRequestDto loginRequestDto,
        CancellationToken cancellationToken = default)
    {
        var validatedRequest = AuthValidationHelper.ValidateLoginRequest(loginRequestDto);
        var user = await _unitOfWork.Auth.GetUserByEmailAsync(validatedRequest.Email, cancellationToken);

        if (user is null ||
            string.IsNullOrWhiteSpace(user.PasswordHash) ||
            !_passwordHasher.VerifyPassword(validatedRequest.Password, user.PasswordHash))
        {
            throw new InvalidCredentialsException();
        }

        if (!string.Equals(user.AccountStatus, ActiveAccountStatus, StringComparison.Ordinal))
        {
            throw new AccountStatusNotAllowedException(user.AccountStatus);
        }

        var now = DateTimeOffset.UtcNow;
        var roleCodes = await _unitOfWork.Auth.GetActiveRoleCodesByUserIdAsync(user.UserId, cancellationToken);
        var refreshToken = CreateRefreshToken(user.UserId, validatedRequest.RememberMe, now);

        user.LastLoginAt = now;
        user.UpdatedAt = now;

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            await _unitOfWork.Auth.AddRefreshTokenAsync(refreshToken.Entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return BuildAuthSessionResult(user, roleCodes, refreshToken.Token, refreshToken.Entity.ExpiresAt, validatedRequest.RememberMe, now);
    }

    public async Task<AuthSessionResult> RefreshAsync(
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new InvalidRefreshTokenException();
        }

        var now = DateTimeOffset.UtcNow;
        var tokenHash = ComputeTokenHash(refreshToken);
        var persistedRefreshToken = await _unitOfWork.Auth.GetRefreshTokenByHashAsync(tokenHash, cancellationToken);

        if (persistedRefreshToken is null ||
            persistedRefreshToken.InvalidatedAt is not null ||
            persistedRefreshToken.ExpiresAt <= now)
        {
            throw new InvalidRefreshTokenException();
        }

        var user = await _unitOfWork.Auth.GetUserByIdAsync(persistedRefreshToken.UserId, cancellationToken);
        if (user is null)
        {
            throw new InvalidRefreshTokenException();
        }

        if (!string.Equals(user.AccountStatus, ActiveAccountStatus, StringComparison.Ordinal))
        {
            persistedRefreshToken.InvalidatedAt = now;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            throw new AccountStatusNotAllowedException(user.AccountStatus);
        }

        var roleCodes = await _unitOfWork.Auth.GetActiveRoleCodesByUserIdAsync(user.UserId, cancellationToken);
        var rotatedRefreshToken = CreateRefreshToken(user.UserId, persistedRefreshToken.IsPersistent, now);

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            persistedRefreshToken.InvalidatedAt = now;
            await _unitOfWork.Auth.AddRefreshTokenAsync(rotatedRefreshToken.Entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return BuildAuthSessionResult(
            user,
            roleCodes,
            rotatedRefreshToken.Token,
            rotatedRefreshToken.Entity.ExpiresAt,
            rotatedRefreshToken.Entity.IsPersistent,
            now);
    }

    public async Task LogoutAsync(string? refreshToken, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return;
        }

        var tokenHash = ComputeTokenHash(refreshToken);
        var persistedRefreshToken = await _unitOfWork.Auth.GetRefreshTokenByHashAsync(tokenHash, cancellationToken);

        if (persistedRefreshToken is null || persistedRefreshToken.InvalidatedAt is not null)
        {
            return;
        }

        persistedRefreshToken.InvalidatedAt = DateTimeOffset.UtcNow;
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task<SignupResponseDto> SignupAsync(
        SignupRequestDto signupRequestDto,
        CancellationToken cancellationToken = default)
    {
        var validatedRequest = AuthValidationHelper.ValidateSignupRequest(signupRequestDto, _signupPolicyOptions);

        if (await _unitOfWork.Auth.EmailExistsAsync(validatedRequest.Email, cancellationToken))
        {
            throw new DuplicateUserCredentialException("Email");
        }

        if (validatedRequest.MobileNumber is not null &&
            await _unitOfWork.Auth.MobileNumberExistsAsync(validatedRequest.MobileNumber, cancellationToken))
        {
            throw new DuplicateUserCredentialException("Mobile number");
        }

        var roleCodes = validatedRequest.RoleApplications.Select(application => application.RoleCode).ToArray();
        var roles = await LoadSupportedRolesAsync(roleCodes, cancellationToken);
        var now = DateTimeOffset.UtcNow;
        var user = BuildUser(validatedRequest, now);
        var emailVerificationToken = CreateEmailVerificationToken(user.UserId, now);
        var verificationUrl = BuildEmailVerificationUrl(emailVerificationToken.Token);
        var requestedRoles = new List<SignupRoleApplicationResponseDto>();

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            await _unitOfWork.Auth.AddUserAsync(user, cancellationToken);

            foreach (var policyAcceptance in BuildPolicyAcceptances(user.UserId, validatedRequest, now))
            {
                await _unitOfWork.Auth.AddUserPolicyAcceptanceAsync(policyAcceptance, cancellationToken);
            }

            foreach (var application in validatedRequest.RoleApplications)
            {
                var role = roles[application.RoleCode];
                var kycApplication = BuildKycApplication(user.UserId, role.RoleId, now);

                await _unitOfWork.Auth.AddKycApplicationAsync(kycApplication, cancellationToken);

                switch (application.RoleCode)
                {
                    case BuyerRoleCode:
                        await _unitOfWork.Auth.AddBuyerProfileAsync(
                            BuildBuyerProfile(user.UserId, application, validatedRequest, now),
                            cancellationToken);
                        break;
                    case FarmerRoleCode:
                        await _unitOfWork.Auth.AddFarmerProfileAsync(
                            BuildFarmerProfile(user.UserId, application, now),
                            cancellationToken);
                        break;
                }

                requestedRoles.Add(new SignupRoleApplicationResponseDto
                {
                    RoleCode = application.RoleCode,
                    KycApplicationId = kycApplication.KycApplicationId,
                    ApplicationStatus = kycApplication.ApplicationStatus
                });
            }

            await _unitOfWork.Auth.AddEmailVerificationTokenAsync(emailVerificationToken.Entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _emailVerificationNotifier.NotifyAsync(user.Email!, verificationUrl, cancellationToken);
        }, cancellationToken);

        return new SignupResponseDto
        {
            UserId = user.UserId,
            Email = user.Email ?? string.Empty,
            MobileNumber = user.MobileNumber,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            IsEmailVerified = user.IsEmailVerified,
            AccountStatus = user.AccountStatus,
            RequestedRoles = requestedRoles
        };
    }

    public async Task<ResendEmailVerificationResponseDto> ResendVerificationAsync(
        ResendEmailVerificationRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        var email = AuthValidationHelper.ValidateEmailAddress(requestDto.Email);
        var user = await _unitOfWork.Auth.GetUserByEmailAsync(email, cancellationToken);

        if (user is null || user.IsEmailVerified)
        {
            return CreateAcceptedResendResponse();
        }

        var now = DateTimeOffset.UtcNow;
        var latestToken = await _unitOfWork.Auth.GetLatestEmailVerificationTokenByUserIdAsync(user.UserId, cancellationToken);
        if (latestToken is not null && latestToken.CreatedAt.AddSeconds(_emailVerificationOptions.ResendCooldownSeconds) > now)
        {
            return CreateAcceptedResendResponse();
        }

        var token = CreateEmailVerificationToken(user.UserId, now);
        var verificationUrl = BuildEmailVerificationUrl(token.Token);

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            var pendingTokens = await _unitOfWork.Auth.GetPendingEmailVerificationTokensByUserIdAsync(user.UserId, cancellationToken);
            InvalidateTokens(pendingTokens, now);

            await _unitOfWork.Auth.AddEmailVerificationTokenAsync(token.Entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _emailVerificationNotifier.NotifyAsync(user.Email!, verificationUrl, cancellationToken);
        }, cancellationToken);

        return CreateAcceptedResendResponse();
    }

    public async Task<EmailVerificationStatusResponseDto> VerifyEmailAsync(
        VerifyEmailRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        var token = AuthValidationHelper.ValidateVerificationToken(requestDto.Token);
        var now = DateTimeOffset.UtcNow;
        var tokenHash = ComputeTokenHash(token);
        var verificationToken = await _unitOfWork.Auth.GetEmailVerificationTokenByHashAsync(tokenHash, cancellationToken);

        if (verificationToken is null)
        {
            return CreateInvalidVerificationResponse();
        }

        var user = await _unitOfWork.Auth.GetUserByIdAsync(verificationToken.UserId, cancellationToken);
        if (user is null)
        {
            return CreateInvalidVerificationResponse();
        }

        if (verificationToken.ConsumedAt is not null && user.IsEmailVerified)
        {
            return CreateAlreadyVerifiedResponse(user, verificationToken.ConsumedAt);
        }

        if (verificationToken.InvalidatedAt is not null || verificationToken.ExpiresAt < now)
        {
            return CreateInvalidVerificationResponse();
        }

        if (user.IsEmailVerified)
        {
            return CreateAlreadyVerifiedResponse(user, verificationToken.ConsumedAt ?? user.UpdatedAt);
        }

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            user.IsEmailVerified = true;
            user.AccountStatus = ActiveAccountStatus;
            user.UpdatedAt = now;
            verificationToken.ConsumedAt = now;

            var pendingTokens = await _unitOfWork.Auth.GetPendingEmailVerificationTokensByUserIdAsync(user.UserId, cancellationToken);
            InvalidateTokens(
                pendingTokens.Where(tokenEntity => tokenEntity.EmailVerificationTokenId != verificationToken.EmailVerificationTokenId),
                now);

            var kycApplications = await _unitOfWork.Auth.GetKycApplicationsByUserIdAsync(user.UserId, cancellationToken);
            foreach (var kycApplication in kycApplications.Where(application =>
                         application.ApplicationStatus == PendingEmailVerificationStatus))
            {
                kycApplication.ApplicationStatus = PendingReviewStatus;
                kycApplication.UpdatedAt = now;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return new EmailVerificationStatusResponseDto
        {
            Status = VerifiedStatus,
            UserId = user.UserId,
            Email = user.Email ?? string.Empty,
            IsEmailVerified = true,
            VerifiedAt = now
        };
    }

    private async Task<Dictionary<string, Role>> LoadSupportedRolesAsync(
        IEnumerable<string> roleCodes,
        CancellationToken cancellationToken)
    {
        var result = new Dictionary<string, Role>(StringComparer.Ordinal);

        foreach (var roleCode in roleCodes.Distinct(StringComparer.Ordinal))
        {
            var role = await _unitOfWork.Auth.GetRoleByCodeAsync(roleCode, cancellationToken);
            if (role is null || !IsSupportedSignupRole(role.RoleCode))
            {
                throw new InvalidSignupRequestException($"Role code {roleCode} is invalid or not supported for signup.");
            }

            result[roleCode] = role;
        }

        return result;
    }

    private static bool IsSupportedSignupRole(string roleCode)
    {
        return roleCode is BuyerRoleCode or FarmerRoleCode;
    }

    private GeneratedVerificationToken CreateEmailVerificationToken(Guid userId, DateTimeOffset now)
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(24);
        var token = Convert.ToHexString(tokenBytes);

        return new GeneratedVerificationToken(
            token,
            new EmailVerificationToken
            {
                EmailVerificationTokenId = Guid.NewGuid(),
                UserId = userId,
                TokenHash = ComputeTokenHash(token),
                ExpiresAt = now.AddHours(_emailVerificationOptions.TokenLifetimeHours),
                CreatedAt = now
            });
    }

    private GeneratedRefreshToken CreateRefreshToken(Guid userId, bool isPersistent, DateTimeOffset now)
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(32);
        var token = Convert.ToHexString(tokenBytes);

        return new GeneratedRefreshToken(
            token,
            new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = userId,
                TokenHash = ComputeTokenHash(token),
                IsPersistent = isPersistent,
                ExpiresAt = now.AddDays(_authOptions.RefreshTokenLifetimeDays),
                CreatedAt = now
            });
    }

    private static string ComputeTokenHash(string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var hash = SHA256.HashData(tokenBytes);
        return Convert.ToHexString(hash);
    }

    private AuthSessionResult BuildAuthSessionResult(
        User user,
        IReadOnlyList<string> roleCodes,
        string refreshToken,
        DateTimeOffset refreshTokenExpiresAt,
        bool persistRefreshToken,
        DateTimeOffset issuedAt)
    {
        var accessTokenExpiresAt = issuedAt.AddMinutes(_authOptions.AccessTokenLifetimeMinutes);

        return new AuthSessionResult(
            new SessionResponseDto
            {
                AccessToken = CreateAccessToken(user, roleCodes, issuedAt, accessTokenExpiresAt),
                AccessTokenExpiresAt = accessTokenExpiresAt,
                User = new SessionUserResponseDto
                {
                    UserId = user.UserId,
                    Email = user.Email ?? string.Empty,
                    MobileNumber = user.MobileNumber,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DisplayName = user.DisplayName,
                    IsEmailVerified = user.IsEmailVerified,
                    AccountStatus = user.AccountStatus,
                    LastLoginAt = user.LastLoginAt,
                    Roles = roleCodes
                }
            },
            refreshToken,
            refreshTokenExpiresAt,
            persistRefreshToken);
    }

    private string CreateAccessToken(
        User user,
        IReadOnlyList<string> roleCodes,
        DateTimeOffset issuedAt,
        DateTimeOffset expiresAt)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SigningKey.Trim()));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new(JwtRegisteredClaimNames.FamilyName, user.LastName)
        };

        if (!string.IsNullOrWhiteSpace(user.DisplayName))
        {
            claims.Add(new Claim("display_name", user.DisplayName));
        }

        foreach (var roleCode in roleCodes)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleCode));
        }

        var token = new JwtSecurityToken(
            issuer: _authOptions.Issuer,
            audience: _authOptions.Audience,
            claims: claims,
            notBefore: issuedAt.UtcDateTime,
            expires: expiresAt.UtcDateTime,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private User BuildUser(ValidatedSignupRequest validatedRequest, DateTimeOffset now)
    {
        return new User
        {
            UserId = Guid.NewGuid(),
            Email = validatedRequest.Email,
            MobileNumber = validatedRequest.MobileNumber,
            PasswordHash = _passwordHasher.HashPassword(validatedRequest.Password),
            FirstName = validatedRequest.FirstName,
            LastName = validatedRequest.LastName,
            DisplayName = validatedRequest.DisplayName,
            IsEmailVerified = false,
            IsMobileVerified = false,
            AccountStatus = PendingEmailVerificationAccountStatus,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static BuyerProfile BuildBuyerProfile(
        Guid userId,
        ValidatedRoleApplication roleApplication,
        ValidatedSignupRequest validatedRequest,
        DateTimeOffset now)
    {
        return new BuyerProfile
        {
            BuyerProfileId = Guid.NewGuid(),
            UserId = userId,
            BusinessName = roleApplication.Name,
            ContactPersonName = $"{validatedRequest.FirstName} {validatedRequest.LastName}".Trim(),
            BusinessType = roleApplication.BusinessType ?? string.Empty,
            BusinessLocationText = roleApplication.LocationText,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static FarmerProfile BuildFarmerProfile(
        Guid userId,
        ValidatedRoleApplication roleApplication,
        DateTimeOffset now)
    {
        return new FarmerProfile
        {
            FarmerProfileId = Guid.NewGuid(),
            UserId = userId,
            FarmName = roleApplication.Name,
            FarmLocationText = roleApplication.LocationText,
            IsPublic = true,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static KycApplication BuildKycApplication(Guid userId, Guid roleId, DateTimeOffset now)
    {
        return new KycApplication
        {
            KycApplicationId = Guid.NewGuid(),
            UserId = userId,
            RoleId = roleId,
            ApplicationStatus = PendingEmailVerificationStatus,
            SubmittedAt = now,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private IEnumerable<UserPolicyAcceptance> BuildPolicyAcceptances(
        Guid userId,
        ValidatedSignupRequest validatedRequest,
        DateTimeOffset now)
    {
        yield return new UserPolicyAcceptance
        {
            UserPolicyAcceptanceId = Guid.NewGuid(),
            UserId = userId,
            PolicyType = TermsPolicyType,
            PolicyVersion = validatedRequest.TermsVersion,
            AcceptedAt = now,
            CreatedAt = now
        };

        yield return new UserPolicyAcceptance
        {
            UserPolicyAcceptanceId = Guid.NewGuid(),
            UserId = userId,
            PolicyType = PrivacyPolicyType,
            PolicyVersion = validatedRequest.PrivacyVersion,
            AcceptedAt = now,
            CreatedAt = now
        };
    }

    private string BuildEmailVerificationUrl(string token)
    {
        var baseUri = new Uri(AppendTrailingSlash(_frontendOptions.ClientAppBaseUrl), UriKind.Absolute);
        var verificationUri = new Uri(baseUri, _emailVerificationOptions.VerificationPath.TrimStart('/'));
        var builder = new UriBuilder(verificationUri)
        {
            Query = $"token={Uri.EscapeDataString(token)}"
        };

        return builder.Uri.ToString();
    }

    private static string AppendTrailingSlash(string value)
    {
        return value.EndsWith("/", StringComparison.Ordinal) ? value : $"{value}/";
    }

    private static void InvalidateTokens(IEnumerable<EmailVerificationToken> tokens, DateTimeOffset now)
    {
        foreach (var token in tokens)
        {
            token.InvalidatedAt ??= now;
        }
    }

    private static ResendEmailVerificationResponseDto CreateAcceptedResendResponse()
    {
        return new ResendEmailVerificationResponseDto
        {
            Status = DispatchAcceptedStatus
        };
    }

    private static EmailVerificationStatusResponseDto CreateInvalidVerificationResponse()
    {
        return new EmailVerificationStatusResponseDto
        {
            Status = InvalidOrExpiredStatus,
            UserId = Guid.Empty,
            Email = string.Empty,
            IsEmailVerified = false
        };
    }

    private static EmailVerificationStatusResponseDto CreateAlreadyVerifiedResponse(
        User user,
        DateTimeOffset? verifiedAt)
    {
        return new EmailVerificationStatusResponseDto
        {
            Status = AlreadyVerifiedStatus,
            UserId = user.UserId,
            Email = user.Email ?? string.Empty,
            IsEmailVerified = true,
            VerifiedAt = verifiedAt
        };
    }

    private sealed record GeneratedVerificationToken(string Token, EmailVerificationToken Entity);
    private sealed record GeneratedRefreshToken(string Token, RefreshToken Entity);
}
