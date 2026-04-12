using APICatalogo.Repositories;
using APICatalogo.WebAPI.Repositories;
using APICatalogo.WebAPI.Repositories.Interfaces;

namespace APICatalogo.WebAPI.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
