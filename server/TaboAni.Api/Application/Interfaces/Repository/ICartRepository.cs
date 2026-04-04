using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Interfaces.Repository;

public interface ICartRepository
{
    Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> IsFarmerProfileOwnedByUserAsync(Guid farmerProfileId, Guid userId, CancellationToken cancellationToken = default);

    Task<Cart?> GetCartByUserIdForUpdateAsync(Guid userId, CancellationToken cancellationToken = default);

    Task AddCartAsync(Cart cart, CancellationToken cancellationToken = default);

    Task<CartItem?> GetCartItemByIdForUpdateAsync(Guid cartItemId, CancellationToken cancellationToken = default);

    Task<CartItem?> GetCartItemByCartIdAndListingIdForUpdateAsync(
        Guid cartId,
        Guid produceListingId,
        CancellationToken cancellationToken = default);

    Task AddCartItemAsync(CartItem cartItem, CancellationToken cancellationToken = default);

    void RemoveCartItem(CartItem cartItem);

    Task<CartListingSnapshotDto?> GetListingSnapshotAsync(
        Guid produceListingId,
        CancellationToken cancellationToken = default);

    Task<ActiveCartQueryResultDto?> GetActiveCartAsync(Guid userId, CancellationToken cancellationToken = default);
}
