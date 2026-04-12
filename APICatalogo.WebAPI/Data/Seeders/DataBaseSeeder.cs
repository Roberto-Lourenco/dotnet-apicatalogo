using APICatalogo.Context;

namespace APICatalogo.WebAPI.Data.Seeders;

internal static class DatabaseSeeder
{
    public static async Task SeedAsync(ApiCatalogoContext context)
    {
        if (!context.Categories.Any())
        {
            var categories = FakeDataFactory.GenerateCategories();
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            var categoryIds = categories.Select(c => c.Id).ToList();

            var products = FakeDataFactory.GenerateProducts(categoryIds);
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
