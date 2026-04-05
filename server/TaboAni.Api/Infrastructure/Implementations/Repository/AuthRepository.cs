using Microsoft.EntityFrameworkCore;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Data;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Infrastructure.Implementations.Repository;

public sealed class AuthRepository(AppDbContext context) : IAuthRepository
{
    private readonly AppDbContext _context = context;

    public Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return _context.Users.AnyAsync(user => user.Email == email, cancellationToken);
    }

    public Task<bool> MobileNumberExistsAsync(string mobileNumber, CancellationToken cancellationToken = default)
    {
        return _context.Users.AnyAsync(user => user.MobileNumber == mobileNumber, cancellationToken);
    }

    public Task<Role?> GetRoleByCodeAsync(string roleCode, CancellationToken cancellationToken = default)
    {
        return _context.Roles.SingleOrDefaultAsync(role => role.RoleCode == roleCode, cancellationToken);
    }

    public Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return _context.Users.SingleOrDefaultAsync(user => user.Email == email, cancellationToken);
    }

    public Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _context.Users.SingleOrDefaultAsync(user => user.UserId == userId, cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetActiveRoleCodesByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.UserRoles
            .AsNoTracking()
            .Where(userRole => userRole.UserId == userId && userRole.IsActive)
            .Join(
                _context.Roles.AsNoTracking(),
                userRole => userRole.RoleId,
                role => role.RoleId,
                (_, role) => role.RoleCode)
            .OrderBy(roleCode => roleCode)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<KycApplication>> GetKycApplicationsByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.KycApplications
            .Where(application => application.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        _context.Users.Add(user);
        return Task.CompletedTask;
    }

    public Task AddUserPolicyAcceptanceAsync(
        UserPolicyAcceptance userPolicyAcceptance,
        CancellationToken cancellationToken = default)
    {
        _context.UserPolicyAcceptances.Add(userPolicyAcceptance);
        return Task.CompletedTask;
    }

    public Task AddUserRoleAsync(UserRole userRole, CancellationToken cancellationToken = default)
    {
        _context.UserRoles.Add(userRole);
        return Task.CompletedTask;
    }

    public Task AddRefreshTokenAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
    {
        _context.RefreshTokens.Add(refreshToken);
        return Task.CompletedTask;
    }

    public Task AddBuyerProfileAsync(BuyerProfile buyerProfile, CancellationToken cancellationToken = default)
    {
        _context.BuyerProfiles.Add(buyerProfile);
        return Task.CompletedTask;
    }

    public Task AddFarmerProfileAsync(FarmerProfile farmerProfile, CancellationToken cancellationToken = default)
    {
        _context.FarmerProfiles.Add(farmerProfile);
        return Task.CompletedTask;
    }

    public Task AddDistributorProfileAsync(DistributorProfile distributorProfile, CancellationToken cancellationToken = default)
    {
        _context.DistributorProfiles.Add(distributorProfile);
        return Task.CompletedTask;
    }

    public Task AddKycApplicationAsync(KycApplication kycApplication, CancellationToken cancellationToken = default)
    {
        _context.KycApplications.Add(kycApplication);
        return Task.CompletedTask;
    }

    public Task AddEmailVerificationTokenAsync(
        EmailVerificationToken emailVerificationToken,
        CancellationToken cancellationToken = default)
    {
        _context.EmailVerificationTokens.Add(emailVerificationToken);
        return Task.CompletedTask;
    }

    public Task<EmailVerificationToken?> GetEmailVerificationTokenByHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default)
    {
        return _context.EmailVerificationTokens
            .SingleOrDefaultAsync(token => token.TokenHash == tokenHash, cancellationToken);
    }

    public Task<EmailVerificationToken?> GetLatestEmailVerificationTokenByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return _context.EmailVerificationTokens
            .Where(token => token.UserId == userId)
            .OrderByDescending(token => token.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<EmailVerificationToken>> GetPendingEmailVerificationTokensByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.EmailVerificationTokens
            .Where(token => token.UserId == userId &&
                            token.ConsumedAt == null &&
                            token.InvalidatedAt == null)
            .ToListAsync(cancellationToken);
    }

    public Task<RefreshToken?> GetRefreshTokenByHashAsync(
        string tokenHash,
        CancellationToken cancellationToken = default)
    {
        return _context.RefreshTokens
            .SingleOrDefaultAsync(token => token.TokenHash == tokenHash, cancellationToken);
    }
}
