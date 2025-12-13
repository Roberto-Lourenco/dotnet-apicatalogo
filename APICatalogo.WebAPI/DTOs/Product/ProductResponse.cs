namespace APICatalogo.DTOs.Product;

public sealed record ProductResponse(
    int Id,
    string Name,
    decimal Price,
    string ImgUrl,
    float AvailableQuantity,
    int CategoryId,
    DateTimeOffset CreatedAt
);
