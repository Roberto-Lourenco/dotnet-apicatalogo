namespace APICatalogo.DTOs.Category;

public sealed record CategoryResponse(
    int Id,
    string Name,
    string ImgUrl,
    DateTimeOffset CreatedAt
);
