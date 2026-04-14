using System.ComponentModel.DataAnnotations;

namespace APICatalogo.WebAPI.DTOs.Products;

public sealed record UpdateProductRequest(
    [Required(ErrorMessage = "O ID do produto é obrigatório.")]
    int Id,

    [StringLength(255, MinimumLength = 2, ErrorMessage = "O nome deve ter no mínimo {2} e no máximo {1} caracteres.")]
    string? Name,

    [StringLength(300, MinimumLength = 3, ErrorMessage = "A descrição deve ter no mínimo {2} e no máximo {1} caracteres.")]
    string? Description,

    [Range(0.50, 10000, ErrorMessage = "O preço deve estar entre {1} e {2}.")]
    decimal? Price,

    [StringLength(150, MinimumLength = 2, ErrorMessage = "A URL da imagem deve ter no mínimo {2} e no máximo {1} caracteres.")]
    string? ImgUrl,

    [Range(1, 10000, ErrorMessage = "O estoque deve ter no mínimo {1} e no máximo {2}.")]
    int? AvailableQuantity,

    int? CategoryId
);
