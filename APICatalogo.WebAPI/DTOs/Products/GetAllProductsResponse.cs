using APICatalogo.Entities;

namespace APICatalogo.WebAPI.DTOs.Products;

public sealed record GetAllProductsResponse(
    int Id,
    string Name,
    decimal Price,
    string ImgUrl)
{
    public static IEnumerable<GetAllProductsResponse> MapToDto(IEnumerable<Product> products)
    {
        if (products == null || !products.Any())
            return new List<GetAllProductsResponse>();

        return products.Select(product =>
            new GetAllProductsResponse(
                product.Id,
                product.Name,
                product.Price,
                product.ImgUrl
            ));
    }
}
