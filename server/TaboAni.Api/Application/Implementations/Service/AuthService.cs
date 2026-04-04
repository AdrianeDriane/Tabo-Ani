using System.Security.Cryptography;
using System.Text;
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
    IEmailVerificationNotifier emailVerificationNotifier) : IAuthService
{
    private const string BuyerRoleCode = "BUYER";
    private const string FarmerRoleCode = "FARMER";
    private const string ActiveAccountStatus = "ACTIVE";
    private const string PendingEmailVerificationStatus = "PENDING_EMAIL_VERIFICATION";
    private const string PendingReviewStatus = "PENDING_REVIEW";
    private static readonly TimeSpan VerificationTokenLifetime = TimeSpan.FromHours(24);

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IEmailVerificationNotifier _emailVerificationNotifier = emailVerificationNotifier;

    public async Task<SignupResponseDto> SignupAsync(
        SignupRequestDto signupRequestDto,
        CancellationToken cancellationToken = default)
    {
        var validatedRequest = AuthValidationHelper.ValidateSignupRequest(signupRequestDto);

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
        var requestedRoles = new List<SignupRoleApplicationResponseDto>();

        await _unitOfWork.Auth.AddUserAsync(user, cancellationToken);

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

        await _emailVerificationNotifier.NotifyAsync(user.Email!, emailVerificationToken.Token, cancellationToken);

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
            RequestedRoles = requestedRoles,
            EmailVerificationTokenPreview = emailVerificationToken.Token
        };
    }

    public async Task<EmailVerificationStatusResponseDto> ResendVerificationAsync(
        ResendEmailVerificationRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        var email = AuthValidationHelper.ValidateEmailAddress(requestDto.Email);
        var user = await _unitOfWork.Auth.GetUserByEmailAsync(email, cancellationToken)
            ?? throw new EmailVerificationUserNotFoundException(email);

        if (user.IsEmailVerified)
        {
            return new EmailVerificationStatusResponseDto
            {
                UserId = user.UserId,
                Email = user.Email ?? string.Empty,
                IsEmailVerified = true
            };
        }

        var now = DateTimeOffset.UtcNow;
        var token = CreateEmailVerificationToken(user.UserId, now);

        await _unitOfWork.Auth.AddEmailVerificationTokenAsync(token.Entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _emailVerificationNotifier.NotifyAsync(user.Email!, token.Token, cancellationToken);

        return new EmailVerificationStatusResponseDto
        {
            UserId = user.UserId,
            Email = user.Email ?? string.Empty,
            IsEmailVerified = false,
            EmailVerificationTokenPreview = token.Token
        };
    }

    public async Task<EmailVerificationStatusResponseDto> VerifyEmailAsync(
        VerifyEmailRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        var email = AuthValidationHelper.ValidateEmailAddress(requestDto.Email);
        var token = AuthValidationHelper.ValidateVerificationToken(requestDto.Token);
        var user = await _unitOfWork.Auth.GetUserByEmailAsync(email, cancellationToken)
            ?? throw new EmailVerificationUserNotFoundException(email);

        if (user.IsEmailVerified)
        {
            return new EmailVerificationStatusResponseDto
            {
                UserId = user.UserId,
                Email = user.Email ?? string.Empty,
                IsEmailVerified = true
            };
        }

        var now = DateTimeOffset.UtcNow;
        var tokenHash = ComputeTokenHash(token);
        var verificationToken = await _unitOfWork.Auth.GetActiveEmailVerificationTokenAsync(
            user.UserId,
            tokenHash,
            now,
            cancellationToken);

        if (verificationToken is null)
        {
            throw new EmailVerificationTokenInvalidException("Verification token is invalid or has expired.");
        }

        user.IsEmailVerified = true;
        user.UpdatedAt = now;
        verificationToken.ConsumedAt = now;

        var kycApplications = await _unitOfWork.Auth.GetKycApplicationsByUserIdAsync(user.UserId, cancellationToken);
        foreach (var kycApplication in kycApplications.Where(application =>
                     application.ApplicationStatus == PendingEmailVerificationStatus))
        {
            kycApplication.ApplicationStatus = PendingReviewStatus;
            kycApplication.UpdatedAt = now;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new EmailVerificationStatusResponseDto
        {
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

    private static GeneratedVerificationToken CreateEmailVerificationToken(Guid userId, DateTimeOffset now)
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
                ExpiresAt = now.Add(VerificationTokenLifetime),
                CreatedAt = now
            });
    }

    private static string ComputeTokenHash(string token)
    {
        var tokenBytes = Encoding.UTF8.GetBytes(token);
        var hash = SHA256.HashData(tokenBytes);
        return Convert.ToHexString(hash);
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
            AccountStatus = ActiveAccountStatus,
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

    private sealed record GeneratedVerificationToken(string Token, EmailVerificationToken Entity);
}
