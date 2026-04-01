using Mapster;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Extensions.MappingExtensions;

public static class OrderItemsMappingExtensions
{
    public static OrderItemsRequestDto ToRequestDto(this OrderItem order)
    {
        return order.Adapt<OrderItemsRequestDto>();
    }

    public static OrderItemsResponseDto ToResponseDto(this OrderItem order)
    {
        return order.Adapt<OrderItemsResponseDto>();
    }

    public static OrderItem ToEntity(this OrderItemsRequestDto orderRequestDto)
    {
        return orderRequestDto.Adapt<OrderItem>();
    }
}
