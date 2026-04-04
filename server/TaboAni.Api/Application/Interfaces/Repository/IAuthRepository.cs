using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Interfaces.Repository;

public interface IAuthRepository
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> MobileNumberExistsAsync(string mobileNumber, CancellationToken cancellationToken = default);
    Task<Role?> GetRoleByCodeAsync(string roleCode, CancellationToken cancellationToken = default);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<KycApplication>> GetKycApplicationsByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task AddUserRoleAsync(UserRole userRole, CancellationToken cancellationToken = default);
    Task AddBuyerProfileAsync(BuyerProfile buyerProfile, CancellationToken cancellationToken = default);
    Task AddFarmerProfileAsync(FarmerProfile farmerProfile, CancellationToken cancellationToken = default);
    Task AddDistributorProfileAsync(DistributorProfile distributorProfile, CancellationToken cancellationToken = default);
    Task AddKycApplicationAsync(KycApplication kycApplication, CancellationToken cancellationToken = default);
    Task AddEmailVerificationTokenAsync(EmailVerificationToken emailVerificationToken, CancellationToken cancellationToken = default);
    Task<EmailVerificationToken?> GetActiveEmailVerificationTokenAsync(
        Guid userId,
        string tokenHash,
        DateTimeOffset now,
        CancellationToken cancellationToken = default);
}
