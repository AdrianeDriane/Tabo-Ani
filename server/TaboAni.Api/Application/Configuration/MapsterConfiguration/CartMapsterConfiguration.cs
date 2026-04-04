using Mapster;
using TaboAni.Api.Application.DTOs.Response;

namespace TaboAni.Api.Application.Configuration.MapsterConfiguration;

public sealed class CartMapsterConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CartItemQueryResultDto, CartItemResponseDto>();
    }
}
