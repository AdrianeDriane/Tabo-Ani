using Mapster;
using TaboAni.Api.Application.Configuration.MapsterConfiguration;
using TaboAni.Api.Application.Interfaces.Repository;
using TaboAni.Api.Application.Interfaces.Service;
using TaboAni.Api.Infrastructure.Implementations;
using TaboAni.Api.Infrastructure.Implementations.Repository;
using TaboAni.Api.Infrastructure.Implementations.Service;

namespace TaboAni.Api.Application.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        TypeAdapterConfig.GlobalSettings.Scan(typeof(OrderMapsterConfiguration).Assembly);

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemsRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}
