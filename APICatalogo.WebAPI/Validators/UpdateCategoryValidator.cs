using APICatalogo.WebAPI.DTOs.Categories;
using FluentValidation;

namespace APICatalogo.Validators;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(c => c.Name)
            .Length(2, 100)
            .WithName("Category Name");

        RuleFor(c => c.ImgUrl)
            .Length(2, 150)
            .WithName("Category Image URL");
    }
}
