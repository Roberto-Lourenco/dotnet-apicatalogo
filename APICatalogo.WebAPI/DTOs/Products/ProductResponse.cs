using APICatalogo.Entities;

namespace APICatalogo.WebAPI.DTOs.Products;

public sealed record ProductResponse(
    int Id,
    string Name,
    decimal Price,
    string Description,
    string ImgUrl,
    float AvailableQuantity,
    int CategoryId,
    DateTimeOffset CreatedAt)
{
    public static ProductResponse MapToDto(Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Price,
            product.Description,
            product.ImgUrl,
            product.AvailableQuantity,
            product.CategoryId,
            product.CreatedAt
        );
    }
}
