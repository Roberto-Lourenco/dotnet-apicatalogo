namespace APICatalogo.WebAPI.DTOs.Categories;

public sealed record CreateCategoryRequest(
    string Name,
    string ImgUrl);
