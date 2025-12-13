using APICatalogo.DTOs.Category;
using FluentValidation;

namespace APICatalogo.Validators;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .Length(2, 100)
            .WithName("Category Name");

        RuleFor(c => c.ImgUrl)
            .NotEmpty()
            .Length(2, 150)
            .WithName("Category Image URL");
    }
}
