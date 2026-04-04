using Mapster;
using TaboAni.Api.Application.Configuration.MapsterConfiguration;
using TaboAni.Api.Application.Implementations.Service;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Infrastructure.Implementations;
using TaboAni.Api.Infrastructure.Implementations.Repository;

namespace TaboAni.Api.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(OrderMapsterConfiguration).Assembly);

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IMarketplaceRepository, MarketplaceRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IMarketplaceService, MarketplaceService>();
        services.AddScoped<ICartService, CartService>();

        return services;
    }
}
