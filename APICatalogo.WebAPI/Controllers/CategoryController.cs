using APICatalogo.Entities;
using APICatalogo.WebAPI.DTOs.Categories;
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
    private readonly IUnitOfWork _uow;

    public CategoryController(ICategoryRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    [HttpPost]
    public async Task<Results<Created, ValidationProblem, ProblemHttpResult>> Create(
        CreateCategoryRequest dto, IValidator<CreateCategoryRequest> validator, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var exists = await _repository.ExistsByNameAsync(dto.Name, ct);

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

        await _repository.CreateAsync(category, ct);
        await _uow.CommitAsync(ct);

        return TypedResults.Created();
    }

    [HttpGet("{id:int}")]
    public async Task<Results<Ok<CategoryResponse>, ProblemHttpResult>> GetById(int id)
    {
        var result = await _repository.GetAsync(c => c.Id == id);

        if (result == null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Category with ID {id} not found."
                );
        }

        var response = CategoryResponse.MapToDto(result);

        return TypedResults.Ok(response);
    }

    [HttpGet]
    public async Task<Results<Ok<List<CategoryResponse>>, ProblemHttpResult>> GetAll()
    {
        var categories = await _repository.GetAllAsync();

        var response = CategoryResponse.MapToDto(categories).ToList();

        return TypedResults.Ok(response);
    }

    [HttpGet("{id}/products")]
    public async Task<Results<Ok<GetCategoryWithProductsResponse>, ProblemHttpResult>> GetCategoryWithProducts(
        int id, CancellationToken ct)
    {
        var categories = await _repository.GetCategoryWithProductsAsync(id, ct);

        if (categories is null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Category with ID {id} not found."
                );
        }

        var response = GetCategoryWithProductsResponse.MapToDto(categories);

        return TypedResults.Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<Results<Ok<UpdateCategoryResponse>, ValidationProblem, ProblemHttpResult>> Update(
        int id, UpdateCategoryRequest dto, IValidator<UpdateCategoryRequest> validator, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        if (id != dto.Id)
            return TypedResults.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "The URL ID differs from the request body ID."
                );

        var currentCategory = await _repository.GetAsync(c => c.Id == id, ct);

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

        await _uow.CommitAsync(ct);

        var response = UpdateCategoryResponse.MapToDto(currentCategory);

        return TypedResults.Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<Results<NoContent, ProblemHttpResult>> Delete(
        int id, CancellationToken ct)
    {
        var category = await _repository.GetAsync(c => c.Id == id, ct);

        if (category is null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Category with ID {id} not found."
                );
        }

        await _repository.DeleteAsync(category, ct);
        await _uow.CommitAsync(ct);

        return TypedResults.NoContent();
    }
}
