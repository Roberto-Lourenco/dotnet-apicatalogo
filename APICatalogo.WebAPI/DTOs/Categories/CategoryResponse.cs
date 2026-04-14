using APICatalogo.Entities;

namespace APICatalogo.WebAPI.DTOs.Categories;

public sealed record CategoryResponse(
    int Id,
    string Name,
    string ImgUrl,
    DateTimeOffset CreatedAt)
{
    public static CategoryResponse MapToDto(Category category)
    {
        return new CategoryResponse(
            category.Id,
            category.Name,
            category.ImgUrl,
            category.CreatedAt);
    }

    public static IEnumerable<CategoryResponse> MapToDto(IEnumerable<Category> categories)
    {
        if (categories == null || !categories.Any())
            return new List<CategoryResponse>();

        return categories.Select(MapToDto);
    }
};
