namespace APICatalogo.DTOs.Product;

public sealed record FullContentProductResponse(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string ImgUrl,
    float AvailableQuantity,
    DateTimeOffset CreatedAt,
    int CategoryId
);
