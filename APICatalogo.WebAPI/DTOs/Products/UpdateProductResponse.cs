using APICatalogo.Entities;

namespace APICatalogo.WebAPI.DTOs.Products;

public sealed record UpdateProductResponse(
    int Id,
    string Name,
    decimal Price,
    string ImgUrl,
    float AvailableQuantity,
    int CategoryId,
    DateTimeOffset UpdatedAt)
{
    public static UpdateProductResponse MapToDto(Product product)
    {
        return new UpdateProductResponse(
            product.Id,
            product.Name,
            product.Price,
            product.ImgUrl,
            product.AvailableQuantity,
            product.CategoryId,
            product.UpdatedAt ?? DateTimeOffset.UtcNow
        );
    }
}
