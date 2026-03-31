using Scalar.AspNetCore;

namespace TaboAni.Api.Infrastructure;

public static class ApiDocumentationExtensions
{
    public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }

    public static WebApplication MapApiDocumentation(this WebApplication app)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();

        return app;
    }
}
