using APICatalogo.Middleware;

namespace APICatalogo.Extensions;

public static class GlobalExceptionHandlerExtensions
{
    public static IServiceCollection AddGlobalErrorHandler(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}
