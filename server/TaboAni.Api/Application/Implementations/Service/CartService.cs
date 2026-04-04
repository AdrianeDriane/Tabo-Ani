using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Extensions.MappingExtensions;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Application.Common;
using TaboAni.Api.Application.Guards;
using TaboAni.Api.Application.Validation.Cart;

namespace TaboAni.Api.Application.Implementations.Service;

public sealed class CartService(IUnitOfWork unitOfWork) : ICartService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly CartServiceGuards _guards = new(unitOfWork);

    public async Task<ActiveCartResponseDto> GetActiveCartAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var validatedUserId = CartValidationHelper.ValidateUserId(userId);

        await _guards.EnsureUserExistsAsync(validatedUserId, cancellationToken);

        var (_, wasCreated) = await _guards.GetOrCreateActiveCartForWriteAsync(validatedUserId, cancellationToken);

        if (wasCreated)
        {
            await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(
                _unitOfWork,
                () => _unitOfWork.SaveChangesAsync(cancellationToken),
                cancellationToken);
        }

        var cartQuery = await _guards.GetActiveCartQueryOrThrowAsync(validatedUserId, cancellationToken);
        return cartQuery.ToResponseDto();
    }

    public async Task<ActiveCartResponseDto> AddItemAsync(
        Guid userId,
        AddCartItemRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedUserId = CartValidationHelper.ValidateUserId(userId);
        var validatedRequest = CartValidationHelper.ValidateAddItemRequest(request);

        await _guards.EnsureUserExistsAsync(validatedUserId, cancellationToken);

        var listing = await _guards.GetActiveListingSnapshotOrThrowAsync(validatedRequest.ProduceListingId, cancellationToken);
        var (cart, _) = await _guards.GetOrCreateActiveCartForWriteAsync(validatedUserId, cancellationToken);
        var existingCartItem = await _unitOfWork.Cart.GetCartItemByCartIdAndListingIdForUpdateAsync(
            cart.CartId,
            validatedRequest.ProduceListingId,
            cancellationToken);

        var updatedAt = DateTimeOffset.UtcNow;

        if (existingCartItem is null)
        {
            CartValidationHelper.EnsureQuantityWithinListingRules(validatedRequest.QuantityKg, listing);

            await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
            {
                await _unitOfWork.Cart.AddCartItemAsync(
                    Domain.Entities.CartItem.Create(
                        cart.CartId,
                        validatedRequest.ProduceListingId,
                        validatedRequest.QuantityKg,
                        updatedAt),
                    cancellationToken);

                cart.Touch(updatedAt);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }, cancellationToken);
        }
        else
        {
            var mergedQuantityKg = existingCartItem.QuantityKg + validatedRequest.QuantityKg;
            CartValidationHelper.EnsureQuantityWithinListingRules(mergedQuantityKg, listing);
            existingCartItem.SetQuantity(mergedQuantityKg, updatedAt);

            cart.Touch(updatedAt);

            await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(
                _unitOfWork,
                () => _unitOfWork.SaveChangesAsync(cancellationToken),
                cancellationToken);
        }

        var cartQuery = await _guards.GetActiveCartQueryOrThrowAsync(validatedUserId, cancellationToken);
        return cartQuery.ToResponseDto();
    }

    public async Task<ActiveCartResponseDto> UpdateItemQuantityAsync(
        Guid userId,
        Guid cartItemId,
        UpdateCartItemQuantityRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedUserId = CartValidationHelper.ValidateUserId(userId);
        var validatedCartItemId = CartValidationHelper.ValidateCartItemId(cartItemId);
        var validatedRequest = CartValidationHelper.ValidateUpdateCartItemQuantityRequest(request);

        await _guards.EnsureUserExistsAsync(validatedUserId, cancellationToken);

        var (cart, cartItem) = await _guards.GetOwnedCartItemForMutationAsync(
            validatedUserId,
            validatedCartItemId,
            cancellationToken);

        var listing = await _guards.GetActiveListingSnapshotOrThrowAsync(cartItem.ProduceListingId, cancellationToken);
        CartValidationHelper.EnsureQuantityWithinListingRules(validatedRequest.QuantityKg, listing);

        var updatedAt = DateTimeOffset.UtcNow;
        cartItem.SetQuantity(validatedRequest.QuantityKg, updatedAt);
        cart.Touch(updatedAt);

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(
            _unitOfWork,
            () => _unitOfWork.SaveChangesAsync(cancellationToken),
            cancellationToken);

        var cartQuery = await _guards.GetActiveCartQueryOrThrowAsync(validatedUserId, cancellationToken);
        return cartQuery.ToResponseDto();
    }

    public async Task RemoveItemAsync(Guid userId, Guid cartItemId, CancellationToken cancellationToken = default)
    {
        var validatedUserId = CartValidationHelper.ValidateUserId(userId);
        var validatedCartItemId = CartValidationHelper.ValidateCartItemId(cartItemId);

        await _guards.EnsureUserExistsAsync(validatedUserId, cancellationToken);

        var (cart, cartItem) = await _guards.GetOwnedCartItemForMutationAsync(
            validatedUserId,
            validatedCartItemId,
            cancellationToken);

        await ServiceTransactionExecutor.ExecuteWithinTransactionAsync(_unitOfWork, async () =>
        {
            _unitOfWork.Cart.RemoveCartItem(cartItem);
            cart.Touch(DateTimeOffset.UtcNow);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }
}
