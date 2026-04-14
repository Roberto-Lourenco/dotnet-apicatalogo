using APICatalogo.Entities;

namespace APICatalogo.WebAPI.DTOs.Products;

public sealed record GetProductsByCategoryResponse(
    int Id,
    string Name,
    decimal Price,
    string Description,
    string ImgUrl,
    float AvailableQuantity,
    DateTimeOffset CreatedAt)
{
    public static IEnumerable<GetProductsByCategoryResponse> MapToDto(IEnumerable<Product> products)
    {
        return products.Select(product =>
            new GetProductsByCategoryResponse(
                product.Id,
                product.Name,
                product.Price,
                product.Description,
                product.ImgUrl,
                product.AvailableQuantity,
                product.CreatedAt
            )).ToList();
    }
}
