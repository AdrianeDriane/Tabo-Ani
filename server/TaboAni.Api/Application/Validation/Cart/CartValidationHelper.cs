using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Exceptions;

namespace TaboAni.Api.Application.Validation.Cart;

internal static class CartValidationHelper
{
    public static Guid ValidateUserId(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new InvalidCartException("UserId is required.");
        }

        return userId;
    }

    public static Guid ValidateCartItemId(Guid cartItemId)
    {
        if (cartItemId == Guid.Empty)
        {
            throw new InvalidCartException("CartItemId is required.");
        }

        return cartItemId;
    }

    public static AddCartItemRequestDto ValidateAddItemRequest(AddCartItemRequestDto? request)
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

    public static UpdateCartItemQuantityRequestDto ValidateUpdateCartItemQuantityRequest(
        UpdateCartItemQuantityRequestDto? request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (request.QuantityKg <= 0)
        {
            throw new InvalidCartException("QuantityKg must be greater than 0.");
        }

        return request;
    }

    public static void EnsureQuantityWithinListingRules(decimal quantityKg, CartListingSnapshotDto listing)
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
}
