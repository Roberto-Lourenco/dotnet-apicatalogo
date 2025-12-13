namespace APICatalogo.DTOs.Product;

public sealed record GetAllProductsResponse(
    int Id,
    string Name,
    decimal Price,
    string ImgUrl
);
