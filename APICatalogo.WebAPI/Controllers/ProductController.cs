using APICatalogo.DTOs.Product;
using APICatalogo.Entities;
using APICatalogo.Repositories;
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
    public async Task<Results<Created<ProductResponse>, ValidationProblem>> CreateAsync(CreateProductRequest dto, IValidator<CreateProductRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.ToDictionary());

        var product = new Product(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.ImgUrl,
            dto.AvailableQuantity,
            dto.CategoryId);

        var result = await _repository.CreateAsync(product);

        var response = new ProductResponse(
            result.Id,
            result.Name,
            result.Price,
            result.ImgUrl,
            result.AvailableQuantity,
            result.CategoryId,
            result.CreatedAt);


        return TypedResults.Created($"/api/products/{response.Id}", response);
    }

    [HttpGet("{id:int}")]
    public async Task<Results<Ok<FullContentProductResponse>, ProblemHttpResult>> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

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
    public async Task<Results<Ok<List<GetAllProductsResponse>>, ProblemHttpResult>> GetAll()
    {
        var products =  await _repository.GetAllAsync();

        if (products is null || products.Count == 0)
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
    public async Task<Results<Ok<UpdateProductResponse>, ProblemHttpResult>> UpdateAsync(int id, UpdateProductRequest dto)
    {
        if (id != dto.Id)
        {
            return TypedResults.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "The URL ID differs from the request body ID."
                );
        }

        var currentProduct = await _repository.GetByIdAsync(id);

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

        await _repository.UpdateAsync(currentProduct);

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
    public async Task<Results<NoContent, ProblemHttpResult>> DeleteAsync(int id)
    {
        var productDeleted = await _repository.DeleteAsync(id);

        if (productDeleted is null)
            return TypedResults.Problem(
                statusCode: StatusCodes.Status404NotFound,
                detail: $"Product with ID {id} not found."
            );

        return TypedResults.NoContent();
    }
}
