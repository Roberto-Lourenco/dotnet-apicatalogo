using APICatalogo.Middleware;

namespace APICatalogo.WebAPI.Extensions;

public static class GlobalExceptionHandlerExtensions
{
    public static IServiceCollection AddGlobalErrorHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}
