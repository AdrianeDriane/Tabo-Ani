using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Interfaces.Repository;

public interface IAuthRepository
{
    Task<bool> EmailExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> MobileNumberExistsAsync(string mobileNumber, CancellationToken cancellationToken = default);
    Task<Role?> GetRoleByCodeAsync(string roleCode, CancellationToken cancellationToken = default);
    Task AddUserAsync(User user, CancellationToken cancellationToken = default);
    Task AddUserRoleAsync(UserRole userRole, CancellationToken cancellationToken = default);
    Task AddBuyerProfileAsync(BuyerProfile buyerProfile, CancellationToken cancellationToken = default);
    Task AddFarmerProfileAsync(FarmerProfile farmerProfile, CancellationToken cancellationToken = default);
    Task AddDistributorProfileAsync(DistributorProfile distributorProfile, CancellationToken cancellationToken = default);
}
