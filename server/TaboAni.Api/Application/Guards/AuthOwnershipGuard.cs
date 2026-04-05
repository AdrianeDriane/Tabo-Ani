using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Security;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Application.Guards;

public sealed class AuthOwnershipGuard(ICurrentUserAccessor currentUserAccessor, IUnitOfWork unitOfWork)
{
    private readonly ICurrentUserAccessor _currentUserAccessor = currentUserAccessor;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Guid GetRequiredCurrentUserId()
    {
        return _currentUserAccessor.GetRequiredUserId();
    }

    public void EnsureCurrentUserMatches(Guid requestedUserId)
    {
        var authenticatedUserId = GetRequiredCurrentUserId();

        if (authenticatedUserId != requestedUserId)
        {
            throw new AuthenticatedUserMismatchException(authenticatedUserId, requestedUserId);
        }
    }

    public async Task EnsureCurrentUserOwnsFarmerProfileAsync(
        Guid farmerProfileId,
        CancellationToken cancellationToken = default)
    {
        var authenticatedUserId = GetRequiredCurrentUserId();
        var ownerUserId = await _unitOfWork.Marketplace.GetFarmerProfileOwnerUserIdAsync(farmerProfileId, cancellationToken);

        if (!ownerUserId.HasValue)
        {
            throw new FarmerProfileNotFoundException(farmerProfileId);
        }

        if (ownerUserId.Value != authenticatedUserId)
        {
            throw new FarmerProfileOwnershipException(farmerProfileId, authenticatedUserId);
        }
    }
}
