using Mapster;
using TaboAni.Api.Application.DTOs.Request;
using TaboAni.Api.Application.DTOs.Response;
using TaboAni.Api.Domain.Entities;

namespace TaboAni.Api.Application.Configuration.MapsterConfiguration;

public sealed class OrderMapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Order, OrderRequestDto>().TwoWays();
        config.NewConfig<Order, OrderResponseDto>();
        config.NewConfig<Order, InitialOrderRequestDto>().TwoWays();
    }
}
