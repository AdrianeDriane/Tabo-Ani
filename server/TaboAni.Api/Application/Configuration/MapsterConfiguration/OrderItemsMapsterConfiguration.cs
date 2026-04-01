using Mapster;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Configuration.MapsterConfiguration;

public sealed class OrderItemsMapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<OrderItem, OrderItemsRequestDto>().TwoWays();
        config.NewConfig<OrderItem, OrderItemsResponseDto>().TwoWays();
    }
}