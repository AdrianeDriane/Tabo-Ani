using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Domain.Entities;
using TaboAni.Api.Domain.Exceptions;
using TaboAni.Api.Domain.Validation;

namespace TaboAni.Api.Infrastructure.Implementations.Service;

public sealed class CartService(IUnitOfWork unitOfWork) : ICartService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ActiveCartResponseDto> GetActiveCartAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var validatedUserId = ValidateUserId(userId);

        await EnsureUserExistsAsync(validatedUserId, cancellationToken);

        var (cart, wasCreated) = await GetOrCreateActiveCartForWriteAsync(validatedUserId, cancellationToken);

        // Persist the lazily created cart so future item operations have a stable owner row.
        if (wasCreated)
        {
            await ExecuteWithinTransactionAsync(
                () => _unitOfWork.SaveChangesAsync(cancellationToken),
                cancellationToken);
        }

        return await GetActiveCartOrThrowAsync(validatedUserId, cancellationToken);
    }

    public async Task<ActiveCartResponseDto> AddItemAsync(
        Guid userId,
        AddCartItemRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedUserId = ValidateUserId(userId);
        var validatedRequest = ValidateAddItemRequest(request);

        await EnsureUserExistsAsync(validatedUserId, cancellationToken);

        var listing = await GetActiveListingSnapshotOrThrowAsync(validatedRequest.ProduceListingId, cancellationToken);
        var (cart, _) = await GetOrCreateActiveCartForWriteAsync(validatedUserId, cancellationToken);
        var existingCartItem = await _unitOfWork.Cart.GetCartItemByCartIdAndListingIdForUpdateAsync(
            cart.CartId,
            validatedRequest.ProduceListingId,
            cancellationToken);

        var updatedAt = DateTimeOffset.UtcNow;

        if (existingCartItem is null)
        {
            EnsureQuantityWithinListingRules(validatedRequest.QuantityKg, listing);

            await ExecuteWithinTransactionAsync(async () =>
            {
                await _unitOfWork.Cart.AddCartItemAsync(
                    new CartItem
                    {
                        CartItemId = Guid.NewGuid(),
                        CartId = cart.CartId,
                        ProduceListingId = validatedRequest.ProduceListingId,
                        QuantityKg = validatedRequest.QuantityKg,
                        CreatedAt = updatedAt,
                        UpdatedAt = updatedAt
                    },
                    cancellationToken);

                cart.UpdatedAt = updatedAt;
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }, cancellationToken);
        }
        else
        {
            var mergedQuantityKg = existingCartItem.QuantityKg + validatedRequest.QuantityKg;

            // Revalidate after merging so duplicate adds still respect listing min/max boundaries.
            EnsureQuantityWithinListingRules(mergedQuantityKg, listing);

            existingCartItem.QuantityKg = mergedQuantityKg;
            existingCartItem.UpdatedAt = updatedAt;
            cart.UpdatedAt = updatedAt;

            await ExecuteWithinTransactionAsync(
                () => _unitOfWork.SaveChangesAsync(cancellationToken),
                cancellationToken);
        }

        return await GetActiveCartOrThrowAsync(validatedUserId, cancellationToken);
    }

    public async Task<ActiveCartResponseDto> UpdateItemQuantityAsync(
        Guid userId,
        Guid cartItemId,
        UpdateCartItemQuantityRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validatedUserId = ValidateUserId(userId);
        var validatedCartItemId = ValidateCartItemId(cartItemId);
        var validatedRequest = ValidateUpdateCartItemQuantityRequest(request);

        await EnsureUserExistsAsync(validatedUserId, cancellationToken);

        var (cart, cartItem) = await GetOwnedCartItemForMutationAsync(
            validatedUserId,
            validatedCartItemId,
            cancellationToken);

        var listing = await GetActiveListingSnapshotOrThrowAsync(cartItem.ProduceListingId, cancellationToken);
        EnsureQuantityWithinListingRules(validatedRequest.QuantityKg, listing);

        var updatedAt = DateTimeOffset.UtcNow;
        cartItem.QuantityKg = validatedRequest.QuantityKg;
        cartItem.UpdatedAt = updatedAt;
        cart.UpdatedAt = updatedAt;

        await ExecuteWithinTransactionAsync(
            () => _unitOfWork.SaveChangesAsync(cancellationToken),
            cancellationToken);

        return await GetActiveCartOrThrowAsync(validatedUserId, cancellationToken);
    }

    public async Task RemoveItemAsync(Guid userId, Guid cartItemId, CancellationToken cancellationToken = default)
    {
        var validatedUserId = ValidateUserId(userId);
        var validatedCartItemId = ValidateCartItemId(cartItemId);

        await EnsureUserExistsAsync(validatedUserId, cancellationToken);

        var (cart, cartItem) = await GetOwnedCartItemForMutationAsync(
            validatedUserId,
            validatedCartItemId,
            cancellationToken);

        await ExecuteWithinTransactionAsync(async () =>
        {
            _unitOfWork.Cart.RemoveCartItem(cartItem);
            cart.UpdatedAt = DateTimeOffset.UtcNow;
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }, cancellationToken);
    }

    private async Task ExecuteWithinTransactionAsync(Func<Task> action, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            await action();
            await _unitOfWork.CommitAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackAsync(cancellationToken);
            throw;
        }
    }

    private async Task EnsureUserExistsAsync(Guid userId, CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Cart.UserExistsAsync(userId, cancellationToken))
        {
            throw new UserNotFoundException(userId);
        }
    }

    private async Task<(Cart Cart, bool WasCreated)> GetOrCreateActiveCartForWriteAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.Cart.GetCartByUserIdForUpdateAsync(userId, cancellationToken);

        if (cart is null)
        {
            var now = DateTimeOffset.UtcNow;

            cart = new Cart
            {
                CartId = Guid.NewGuid(),
                UserId = userId,
                CartStatus = CartStatusPolicy.Active,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _unitOfWork.Cart.AddCartAsync(cart, cancellationToken);
            return (cart, true);
        }

        if (!CartStatusPolicy.IsActive(cart.CartStatus))
        {
            throw new CartIntegrityException("Only a single ACTIVE cart is supported for each user.");
        }

        return (cart, false);
    }

    private async Task<Cart> GetExistingActiveCartForWriteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.Cart.GetCartByUserIdForUpdateAsync(userId, cancellationToken)
            ?? throw new CartNotFoundException(userId);

        if (!CartStatusPolicy.IsActive(cart.CartStatus))
        {
            throw new CartIntegrityException("Only a single ACTIVE cart is supported for each user.");
        }

        return cart;
    }

    private async Task<(Cart Cart, CartItem CartItem)> GetOwnedCartItemForMutationAsync(
        Guid userId,
        Guid cartItemId,
        CancellationToken cancellationToken)
    {
        var cartItem = await _unitOfWork.Cart.GetCartItemByIdForUpdateAsync(cartItemId, cancellationToken)
            ?? throw new CartItemNotFoundException(cartItemId);

        // Resolve the item first so existing foreign items surface as forbidden instead of missing.
        var cart = await _unitOfWork.Cart.GetCartByUserIdForUpdateAsync(userId, cancellationToken);

        if (cart is null || cartItem.CartId != cart.CartId)
        {
            throw new CartOwnershipException(cartItemId, userId);
        }

        if (!CartStatusPolicy.IsActive(cart.CartStatus))
        {
            throw new CartIntegrityException("Only a single ACTIVE cart is supported for each user.");
        }

        return (cart, cartItem);
    }

    private async Task<CartListingSnapshotDto> GetActiveListingSnapshotOrThrowAsync(
        Guid produceListingId,
        CancellationToken cancellationToken)
    {
        var listing = await _unitOfWork.Cart.GetListingSnapshotAsync(produceListingId, cancellationToken)
            ?? throw new ListingNotFoundException(produceListingId);

        if (!string.Equals(listing.ListingStatus, ListingStatusPolicy.Active, StringComparison.Ordinal))
        {
            throw new InvalidCartException("Only ACTIVE listings can be added or updated in the cart.");
        }

        return listing;
    }

    private async Task<ActiveCartResponseDto> GetActiveCartOrThrowAsync(Guid userId, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.Cart.GetActiveCartAsync(userId, cancellationToken)
            ?? throw new CartIntegrityException("Expected an ACTIVE cart after cart initialization.");

        return ToActiveCartResponse(cart);
    }

    private static Guid ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new InvalidCartException("UserId is required.");
        }

        return userId;
    }

    private static Guid ValidateCartItemId(Guid cartItemId)
    {
        if (cartItemId == Guid.Empty)
        {
            throw new InvalidCartException("CartItemId is required.");
        }

        return cartItemId;
    }

    private static AddCartItemRequestDto ValidateAddItemRequest(AddCartItemRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.ProduceListingId == Guid.Empty)
        {
            throw new InvalidCartException("ProduceListingId is required.");
        }

        if (request.QuantityKg <= 0)
        {
            throw new InvalidCartException("QuantityKg must be greater than 0.");
        }

        return request;
    }

    private static UpdateCartItemQuantityRequestDto ValidateUpdateCartItemQuantityRequest(
        UpdateCartItemQuantityRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.QuantityKg <= 0)
        {
            throw new InvalidCartException("QuantityKg must be greater than 0.");
        }

        return request;
    }

    private static void EnsureQuantityWithinListingRules(decimal quantityKg, CartListingSnapshotDto listing)
    {
        if (quantityKg < listing.MinimumOrderKg)
        {
            throw new InvalidCartException(
                $"QuantityKg must be greater than or equal to the listing minimum of {listing.MinimumOrderKg}.");
        }

        if (listing.MaximumOrderKg.HasValue && quantityKg > listing.MaximumOrderKg.Value)
        {
            throw new InvalidCartException(
                $"QuantityKg must be less than or equal to the listing maximum of {listing.MaximumOrderKg.Value}.");
        }
    }

    private static ActiveCartResponseDto ToActiveCartResponse(ActiveCartQueryResultDto cart)
    {
        var items = cart.Items
            .Select(item => new CartItemResponseDto(
                item.CartItemId,
                item.ProduceListingId,
                item.FarmerProfileId,
                item.FarmerFarmName,
                item.ListingTitle,
                item.ProduceName,
                item.PricePerKg,
                item.QuantityKg,
                item.MinimumOrderKg,
                item.MaximumOrderKg,
                item.PrimaryLocationText,
                item.PrimaryImageUrl))
            .ToList();

        return new ActiveCartResponseDto(
            cart.CartId,
            cart.UserId,
            cart.CartStatus,
            items.Count,
            items.Sum(item => item.QuantityKg),
            items);
    }
}
