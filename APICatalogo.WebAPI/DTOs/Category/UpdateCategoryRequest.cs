namespace APICatalogo.DTOs.Category;

public record UpdateCategoryRequest(
    int Id,
    string? Name,
    string? ImgUrl
);
