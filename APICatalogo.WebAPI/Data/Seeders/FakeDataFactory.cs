using APICatalogo.Entities;
using Bogus;

namespace APICatalogo.WebAPI.Data.Seeders;

public static class FakeDataFactory
{
    public static List<Category> GenerateCategories()
    {
        var categoryFaker = new Faker<Category>("pt_BR")
            .CustomInstantiator(f => new Category(
                name: f.Commerce.Department(),
                imgUrl: f.Image.PicsumUrl()));

        return categoryFaker
                .Generate(50)
                .DistinctBy(c => c.Name)
                .Take(8)
                .ToList();
    }

    public static List<Product> GenerateProducts(List<int> validCategoryIds)
    {
        var productFaker = new Faker<Product>("pt_BR")
            .CustomInstantiator(f => new Product(
                name: f.Commerce.ProductName(),
                description: f.Commerce.ProductDescription(),
                price: f.Finance.Amount(10m, 5000m),
                imgUrl: f.Image.PicsumUrl(),
                availableQuantity: f.Random.Int(1, 100),
                categoryId: f.PickRandom(validCategoryIds)
            ));

        return productFaker
            .Generate(50)
            .DistinctBy(c => c.Name)
            .Take(20)
            .ToList();
    }
}
