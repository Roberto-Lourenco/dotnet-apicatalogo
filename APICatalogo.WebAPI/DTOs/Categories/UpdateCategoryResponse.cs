using APICatalogo.Entities;

namespace APICatalogo.WebAPI.DTOs.Categories;

public sealed record UpdateCategoryResponse(
    int Id,
    string Name,
    string ImgUrl,
    DateTimeOffset UpdatedAt)
{
    public static UpdateCategoryResponse MapToDto(Category category)
    {
        return new UpdateCategoryResponse(
            category.Id,
            category.Name,
            category.ImgUrl,
            category.UpdatedAt ?? DateTimeOffset.UtcNow);
    }
}
