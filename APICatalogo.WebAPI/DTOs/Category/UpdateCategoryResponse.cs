namespace APICatalogo.DTOs.Category;

public sealed record UpdateCategoryResponse(
    int Id,
    string Name,
    string ImgUrl,
    DateTimeOffset UpdatedAt
);
