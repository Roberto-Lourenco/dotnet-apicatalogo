namespace APICatalogo.DTOs.Product;

public sealed record UpdateProductResponse(
    int Id,
    string Name,
    decimal Price,
    string ImgUrl,
    float AvailableQuantity,
    int CategoryId,
    DateTimeOffset UpdatedAt
);

