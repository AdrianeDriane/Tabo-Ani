using Mapster;
using TaboAni.Api.Application.DTOs.Response;

namespace TaboAni.Api.Application.Extensions.MappingExtensions;

public static class CartMappingExtensions
{
    public static CartItemResponseDto ToResponseDto(this CartItemQueryResultDto item)
    {
        return item.Adapt<CartItemResponseDto>();
    }

    public static ActiveCartResponseDto ToResponseDto(this ActiveCartQueryResultDto cart)
    {
        var items = cart.Items
            .Select(item => item.ToResponseDto())
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
