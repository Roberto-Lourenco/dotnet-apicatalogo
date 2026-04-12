using APICatalogo.Context;
using APICatalogo.WebAPI.Data.Seeders;

namespace APICatalogo.WebAPI.Extensions;

public static class DataSeedExtensions
{
    public static async Task<IApplicationBuilder> UseDataSeederAsync(this IApplicationBuilder app)
    {
        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

        if (!env.IsDevelopment())
            return app;

        await using var scope = app.ApplicationServices.CreateAsyncScope();

        try
        {
            var context = scope.ServiceProvider.GetRequiredService<ApiCatalogoContext>();
            await DatabaseSeeder.SeedAsync(context);
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Ocorreu um erro ao popular o banco de dados.");
        }

        return app;
    }
}
