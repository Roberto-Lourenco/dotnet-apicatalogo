using APICatalogo.DTOs.Category;
using APICatalogo.Entities;
using APICatalogo.WebAPI.Repositories.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

// Lógica temporária, atualmente sem serviços
[Route("api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repository;

    public CategoryController(ICategoryRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<Results<Created<CategoryResponse>, ValidationProblem, ProblemHttpResult>> CreateAsync(CreateCategoryRequest dto, IValidator<CreateCategoryRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var exists = await _repository.ExistsByNameAsync(dto.Name);
        if (exists)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status409Conflict,
                detail: $"A category with the name '{dto.Name}' already exists.");
        }

        var category = new Category(
            dto.Name,
            dto.ImgUrl
        );

        var result = await _repository.CreateAsync(category);

        var response = new CategoryResponse(
            result.Id,
            result.Name,
            result.ImgUrl,
            result.CreatedAt
        );

        return TypedResults.Created($"/api/categories/{response.Id}", response);
    }

    [HttpGet("{id:int}")]
    public async Task<Results<Ok<CategoryResponse>, ProblemHttpResult>> GetByIdAsync(int id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Category with ID {id} not found."
                );
        }

        var response = new CategoryResponse(
            result.Id,
            result.Name,
            result.ImgUrl,
            result.CreatedAt
        );

        return TypedResults.Ok(response);
    }

    [HttpGet]
    public async Task<Results<Ok<List<CategoryResponse>>, ProblemHttpResult>> GetAll()
    {
        var categories = await _repository.GetAllListAsync();

        if (categories is null || categories.Count == 0)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: "No categories found to display.");
        };

        var response = categories
            .Select(category =>
            new CategoryResponse(
                category.Id,
                category.Name,
                category.ImgUrl,
                category.CreatedAt))
            .ToList();

        return TypedResults.Ok(response);
    }

    [HttpGet("{id}/products")]
    public async Task<Results<Ok<GetCategoryWithProductsResponse>, ProblemHttpResult>>  GetCategoryWithProdutosAsync(int id)
    {
        var categories = await _repository.GetCategoryWithProductsAsync(id);
        if (categories is null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Category with ID {id} not found."
                );
        }

        if (categories.Products is null || categories.Products.Count == 0)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Category with ID {id} has no products."
                );
        }

        var response = new GetCategoryWithProductsResponse(
            categories.Id,
            categories.Name,
            categories.ImgUrl,
            categories.Products.Select(product => new GetProductByCategoryResponse(
                product.Id,
                product.Name,
                product.Price,
                product.ImgUrl,
                product.AvailableQuantity,
                product.CreatedAt)).ToList());

        return TypedResults.Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<Results<Ok<UpdateCategoryResponse>, ValidationProblem, ProblemHttpResult>> UpdateAsync(int id, UpdateCategoryRequest dto, IValidator<UpdateCategoryRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        if (id != dto.Id)
            return TypedResults.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "The URL ID differs from the request body ID."
                );


        var currentCategory = await _repository.GetByIdAsync(id);
        if (currentCategory is null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Category with ID {id} not found."
                );
        }
        currentCategory.Name = dto.Name ?? currentCategory.Name;
        currentCategory.ImgUrl = dto.ImgUrl ?? currentCategory.ImgUrl;
        currentCategory.UpdatedAt = DateTimeOffset.UtcNow;

        await _repository.UpdateAsync(currentCategory);

        var response = new UpdateCategoryResponse(
            currentCategory.Id,
            currentCategory.Name,
            currentCategory.ImgUrl,
            currentCategory.UpdatedAt.Value
        );

        return TypedResults.Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<Results<NoContent, ProblemHttpResult>> DeleteAsync(int id)
    {
        var categoriaDeleted = await _repository.DeleteAsync(id);

        if (categoriaDeleted is null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Category with ID {id} not found."
                );
        }

        return TypedResults.NoContent();
    }
}
