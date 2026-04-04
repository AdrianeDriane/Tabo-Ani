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

    public Task AddUserAsync(User user, CancellationToken cancellationToken = default)
    {
        return _context.Users.AddAsync(user, cancellationToken).AsTask();
    }

    public Task AddUserRoleAsync(UserRole userRole, CancellationToken cancellationToken = default)
    {
        return _context.UserRoles.AddAsync(userRole, cancellationToken).AsTask();
    }

    public Task AddBuyerProfileAsync(BuyerProfile buyerProfile, CancellationToken cancellationToken = default)
    {
        return _context.BuyerProfiles.AddAsync(buyerProfile, cancellationToken).AsTask();
    }

    public Task AddFarmerProfileAsync(FarmerProfile farmerProfile, CancellationToken cancellationToken = default)
    {
        return _context.FarmerProfiles.AddAsync(farmerProfile, cancellationToken).AsTask();
    }

    public Task AddDistributorProfileAsync(DistributorProfile distributorProfile, CancellationToken cancellationToken = default)
    {
        return _context.DistributorProfiles.AddAsync(distributorProfile, cancellationToken).AsTask();
    }
}
