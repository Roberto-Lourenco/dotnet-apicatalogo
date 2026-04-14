namespace APICatalogo.WebAPI.DTOs.Categories;

public sealed record UpdateCategoryRequest(
    int Id,
    string? Name,
    string? ImgUrl);
