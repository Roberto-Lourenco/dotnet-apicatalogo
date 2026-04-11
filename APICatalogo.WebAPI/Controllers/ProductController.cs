using APICatalogo.DTOs.Product;
using APICatalogo.Entities;
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
    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<Results<Ok, ValidationProblem>> Create(
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

        return TypedResults.Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<Results<Ok<FullContentProductResponse>, ProblemHttpResult>> GetById(int id, CancellationToken ct)
    {
        var product = await _repository.GetAsync(p => p.Id == id, ct);

        if (product is null)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Product with ID {id} not found."
                );
        }

        var response = new FullContentProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.ImgUrl,
            product.AvailableQuantity,
            product.CreatedAt,
            product.CategoryId
            );

        return TypedResults.Ok(response);
    }

    [HttpGet]
    public async Task<Results<Ok<List<GetAllProductsResponse>>, ProblemHttpResult>> GetAll(CancellationToken ct)
    {
        var products =  await _repository.GetAllAsync(ct);

        if (products is null || !products.Any())
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: "No products found to display."
                );

        var response = products
            .Select(product => 
            new GetAllProductsResponse(
                product.Id,
                product.Name,
                product.Price,
                product.ImgUrl))
            .ToList();

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

        await _repository.UpdateAsync(currentProduct, ct);

        var response = new UpdateProductResponse(
            currentProduct.Id,
            currentProduct.Name,
            currentProduct.Price,
            currentProduct.ImgUrl,
            currentProduct.AvailableQuantity,
            currentProduct.CategoryId,
            currentProduct.UpdatedAt.Value);


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

        return TypedResults.NoContent();
    }
}
