using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs.Product;

public sealed record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string ImgUrl,
    int AvailableQuantity,
    int CategoryId
);
