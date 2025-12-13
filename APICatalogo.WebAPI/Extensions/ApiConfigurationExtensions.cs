using FluentValidation;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APICatalogo.Extensions;

public static class ApiConfigurationExtensions
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

        services.AddProblemDetails();

        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });

        services.AddEndpointsApiExplorer();

        return services;
    }
}
