using Mapster;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Extensions.MappingExtensions;

public static class OrderMappingExtensions
{
    public static OrderRequestDto ToRequestDto(this Order order)
    {
        return order.Adapt<OrderRequestDto>();
    }

    public static OrderResponseDto ToResponseDto(this Order order)
    {
        return order.Adapt<OrderResponseDto>();
    }

    public static Order ToEntity(this OrderRequestDto orderRequestDto)
    {
        return orderRequestDto.Adapt<Order>();
    }
    
    public static Order ToEntity(this InitialOrderRequestDto orderRequestDto)
    {
        return orderRequestDto.Adapt<Order>();
    }
}
