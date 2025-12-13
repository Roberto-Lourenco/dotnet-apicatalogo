using APICatalogo.DTOs.Product;
using FluentValidation;

namespace APICatalogo.Validators;

internal sealed class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .Length(2, 255)
            .WithName("Product Name");

        RuleFor(p => p.Description)
            .NotEmpty()
            .Length(3, 300)
            .WithName("Product Description");

        RuleFor(p => p.Price)
            .GreaterThan(0)
            .WithName("Product Price");

        RuleFor(p => p.ImgUrl)
            .NotEmpty()
            .Length(2, 150)
            .WithName("Product Image URL");

        RuleFor(p => p.AvailableQuantity)
            .NotEmpty()
            .InclusiveBetween(1, 10000)
            .WithName("Product Available Quantity");

        RuleFor(p => p.CategoryId)
            .GreaterThan(0)
            .WithName("Product Category");
    }
}
