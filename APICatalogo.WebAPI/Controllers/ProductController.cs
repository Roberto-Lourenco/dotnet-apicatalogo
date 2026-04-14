using APICatalogo.Entities;
using APICatalogo.WebAPI.DTOs.Products;
using APICatalogo.WebAPI.Repositories.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace APICatalogo.Controllers;

// Lógica temporária, atualmente sem serviços
[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _uow;
    public ProductController(IProductRepository repository, IUnitOfWork uow)
    {
        _repository = repository;
        _uow = uow;
    }

    [HttpPost]
    public async Task<Results<Created, ValidationProblem>> Create(
        CreateProductRequest dto, IValidator<CreateProductRequest> validator, CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(dto, ct);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var product = new Product(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.ImgUrl,
            dto.AvailableQuantity,
            dto.CategoryId);

        await _repository.CreateAsync(product, ct);
        await _uow.CommitAsync(ct);

        return TypedResults.Created();
    }

    [HttpGet("{id:int}")]
    public async Task<Results<Ok<ProductResponse>, ProblemHttpResult>> GetById(int id, CancellationToken ct)
    {
        var product = await _repository.GetAsync(p => p.Id == id, ct);

        if (product is null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Product with ID {id} not found."
                );
        }

        var response = ProductResponse.MapToDto(product);

        return TypedResults.Ok(response);
    }

    [HttpGet]
    public async Task<Results<Ok<List<GetAllProductsResponse>>, ProblemHttpResult>> GetAll(CancellationToken ct)
    {
        var products = await _repository.GetAllAsync(ct);

        if (products is null || !products.Any())
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: "No products found to display."
                );

        var response = GetAllProductsResponse.MapToDto(products).ToList();

        return TypedResults.Ok(response);
    }

    [HttpPut("{id:int}")]
    public async Task<Results<Ok<UpdateProductResponse>, ProblemHttpResult>> Update(int id, UpdateProductRequest dto, CancellationToken ct)
    {
        if (id != dto.Id)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "The URL ID differs from the request body ID."
                );
        }

        var currentProduct = await _repository.GetAsync(p => p.Id == id, ct);

        if (currentProduct is null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Product with ID {id} not found."
                );
        }

        currentProduct.Name = dto.Name ?? currentProduct.Name;
        currentProduct.Description = dto.Description ?? currentProduct.Description;
        currentProduct.Price = dto.Price ?? currentProduct.Price;
        currentProduct.ImgUrl = dto.ImgUrl ?? currentProduct.ImgUrl;
        currentProduct.AvailableQuantity = dto.AvailableQuantity ?? currentProduct.AvailableQuantity;
        currentProduct.CategoryId = dto.CategoryId ?? currentProduct.CategoryId;
        currentProduct.UpdatedAt = DateTimeOffset.UtcNow;

        await _uow.CommitAsync(ct);

        var response = UpdateProductResponse.MapToDto(currentProduct);

        return TypedResults.Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<Results<NoContent, ProblemHttpResult>> Delete(int id, CancellationToken ct)
    {
        var product = await _repository.GetAsync(p => p.Id == id, ct);

        if (product is null)
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Product with ID {id} not found."
            );

        await _repository.DeleteAsync(product, ct);
        await _uow.CommitAsync(ct);

        return TypedResults.NoContent();
    }
}
