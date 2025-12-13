namespace APICatalogo.DTOs.Category;

public sealed record CreateCategoryRequest(
    string Name,
    string ImgUrl
);
