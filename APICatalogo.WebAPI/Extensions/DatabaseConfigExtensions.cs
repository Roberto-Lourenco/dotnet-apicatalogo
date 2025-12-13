using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Extensions;

public static class DatabaseConfigExtensions
{
    public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Database configuration not found.");

        services.AddDbContext<ApiCatalogoContext>(options =>
            options.UseNpgsql(connectionString)
                   .UseSnakeCaseNamingConvention());

        return services;
    }
}
