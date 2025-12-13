namespace APICatalogo.DTOs.Category;

public sealed record GetCategoryWithProductsResponse(
    int Id,
    string Name,
    string ImgUrl,
    IEnumerable<GetProductByCategoryResponse> Products
);

public sealed record GetProductByCategoryResponse(
    int Id,
    string Name,
    decimal Price,
    string ImgUrl,
    float AvailableQuantity,
    DateTimeOffset CreatedAt
);

