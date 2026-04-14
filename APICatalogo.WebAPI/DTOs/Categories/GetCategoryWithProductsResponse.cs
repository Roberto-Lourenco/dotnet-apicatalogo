using APICatalogo.Entities;
using APICatalogo.WebAPI.DTOs.Products;

namespace APICatalogo.WebAPI.DTOs.Categories;

public sealed record GetCategoryWithProductsResponse(
    int Id,
    string Name,
    string ImgUrl,
    IEnumerable<GetProductsByCategoryResponse> Products)
{
    public static GetCategoryWithProductsResponse MapToDto(Category category)
    {
        return new GetCategoryWithProductsResponse(
            category.Id,
            category.Name,
            category.ImgUrl,
            GetProductsByCategoryResponse.MapToDto(category.Products)
        );
    }
}
