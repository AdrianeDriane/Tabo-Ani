using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Enums;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Application.Guards;

internal sealed class CartServiceGuards(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task EnsureUserExistsAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Cart.UserExistsAsync(userId, cancellationToken))
        {
            throw new UserNotFoundException(userId);
        }
    }

    public async Task<(Cart Cart, bool WasCreated)> GetOrCreateActiveCartForWriteAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.Cart.GetCartByUserIdForUpdateAsync(userId, cancellationToken);

        if (cart is null)
        {
            var now = DateTimeOffset.UtcNow;
            cart = Cart.CreateActive(userId, now);
            await _unitOfWork.Cart.AddCartAsync(cart, cancellationToken);
            return (cart, true);
        }

        cart.EnsureActive();
        return (cart, false);
    }

    public async Task<(Cart Cart, CartItem CartItem)> GetOwnedCartItemForMutationAsync(
        Guid userId,
        Guid cartItemId,
        CancellationToken cancellationToken)
    {
        var cartItem = await _unitOfWork.Cart.GetCartItemByIdForUpdateAsync(cartItemId, cancellationToken)
            ?? throw new CartItemNotFoundException(cartItemId);

        var cart = await _unitOfWork.Cart.GetCartByUserIdForUpdateAsync(userId, cancellationToken);

        if (cart is null || cartItem.CartId != cart.CartId)
        {
            throw new CartOwnershipException(cartItemId, userId);
        }

        cart.EnsureActive();
        return (cart, cartItem);
    }

    public async Task<CartListingSnapshotDto> GetActiveListingSnapshotOrThrowAsync(
        Guid produceListingId,
        CancellationToken cancellationToken)
    {
        var listing = await _unitOfWork.Cart.GetListingSnapshotAsync(produceListingId, cancellationToken)
            ?? throw new ListingNotFoundException(produceListingId);

        if (listing.ListingStatus != ListingStatus.Active)
        {
            throw new InvalidCartException("Only ACTIVE listings can be added or updated in the cart.");
        }

        return listing;
    }

    public async Task EnsureListingIsNotOwnedByUserAsync(
        Guid userId,
        CartListingSnapshotDto listing,
        CancellationToken cancellationToken)
    {
        if (await _unitOfWork.Cart.IsFarmerProfileOwnedByUserAsync(listing.FarmerProfileId, userId, cancellationToken))
        {
            throw new InvalidCartException("You cannot add or update your own listing in your cart.");
        }
    }

    public async Task<ActiveCartQueryResultDto> GetActiveCartQueryOrThrowAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Cart.GetActiveCartAsync(userId, cancellationToken)
            ?? throw new CartIntegrityException("Expected an ACTIVE cart after cart initialization.");
    }
}
