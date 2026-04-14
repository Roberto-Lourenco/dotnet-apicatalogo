namespace APICatalogo.WebAPI.DTOs.Products;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string ImgUrl,
    int AvailableQuantity,
    int CategoryId);
