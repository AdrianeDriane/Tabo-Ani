using TaboAni.Api.Application.Common;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Security;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Application.Validation.Auth;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Application.Implementations.Service;

public sealed class AuthService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher) : IAuthService
{
    private const string BuyerRoleCode = "BUYER";
    private const string FarmerRoleCode = "FARMER";
    private const string DistributorRoleCode = "DISTRIBUTOR";
    private const string ActiveAccountStatus = "ACTIVE";

    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;

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

        var role = await _unitOfWork.Auth.GetRoleByCodeAsync(validatedRequest.RoleCode, cancellationToken);

        if (role is null || !IsSupportedSignupRole(role.RoleCode))
        {
            throw new InvalidSignupRequestException("Role code is invalid or not supported for signup.");
        }

        var now = DateTimeOffset.UtcNow;
        var user = BuildUser(validatedRequest, now);
        var userRole = BuildUserRole(user.UserId, role.RoleId, now);
        var contactPersonName = $"{validatedRequest.FirstName} {validatedRequest.LastName}".Trim();

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            await _unitOfWork.Auth.AddUserAsync(user, cancellationToken);
            await _unitOfWork.Auth.AddUserRoleAsync(userRole, cancellationToken);

            switch (role.RoleCode)
            {
                case BuyerRoleCode:
                    await _unitOfWork.Auth.AddBuyerProfileAsync(
                        BuildBuyerProfile(user.UserId, validatedRequest, contactPersonName, now),
                        cancellationToken);
                    break;
                case FarmerRoleCode:
                    await _unitOfWork.Auth.AddFarmerProfileAsync(
                        BuildFarmerProfile(user.UserId, validatedRequest, now),
                        cancellationToken);
                    break;
                case DistributorRoleCode:
                    await _unitOfWork.Auth.AddDistributorProfileAsync(
                        BuildDistributorProfile(user.UserId, validatedRequest, now),
                        cancellationToken);
                    break;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);

        return new SignupResponseDto
        {
            UserId = user.UserId,
            Email = user.Email ?? string.Empty,
            MobileNumber = user.MobileNumber,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            RoleCode = role.RoleCode,
            AccountStatus = user.AccountStatus
        };
    }

    private static bool IsSupportedSignupRole(string roleCode)
    {
        return roleCode is BuyerRoleCode or FarmerRoleCode or DistributorRoleCode;
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

    private static UserRole BuildUserRole(Guid userId, Guid roleId, DateTimeOffset now)
    {
        return new UserRole
        {
            UserRoleId = Guid.NewGuid(),
            UserId = userId,
            RoleId = roleId,
            AssignedAt = now,
            IsActive = true,
            CreatedAt = now
        };
    }

    private static BuyerProfile BuildBuyerProfile(
        Guid userId,
        ValidatedSignupRequest validatedRequest,
        string contactPersonName,
        DateTimeOffset now)
    {
        return new BuyerProfile
        {
            BuyerProfileId = Guid.NewGuid(),
            UserId = userId,
            BusinessName = validatedRequest.BusinessName,
            ContactPersonName = contactPersonName,
            BusinessType = BuyerRoleCode,
            BusinessLocationText = validatedRequest.LocationText,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static FarmerProfile BuildFarmerProfile(Guid userId, ValidatedSignupRequest validatedRequest, DateTimeOffset now)
    {
        return new FarmerProfile
        {
            FarmerProfileId = Guid.NewGuid(),
            UserId = userId,
            FarmName = validatedRequest.BusinessName,
            FarmLocationText = validatedRequest.LocationText,
            IsPublic = true,
            CreatedAt = now,
            UpdatedAt = now
        };
    }

    private static DistributorProfile BuildDistributorProfile(
        Guid userId,
        ValidatedSignupRequest validatedRequest,
        DateTimeOffset now)
    {
        return new DistributorProfile
        {
            DistributorProfileId = Guid.NewGuid(),
            UserId = userId,
            FleetDisplayName = validatedRequest.BusinessName,
            BaseLocationText = validatedRequest.LocationText,
            IsAvailable = true,
            CreatedAt = now,
            UpdatedAt = now
        };
    }
}
