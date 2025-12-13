namespace APICatalogo.Entities;

public sealed class Product
{
    public Product(
        string name,
        string description,
        decimal price,
        string imgUrl,
        int availableQuantity,
        int categoryId)
    {

        Name = name;
        Description = description;
        Price = price;
        ImgUrl = imgUrl;
        AvailableQuantity = availableQuantity;
        CreatedAt = DateTimeOffset.UtcNow;
        CategoryId = categoryId;
    }

    private Product()
    {
        Name = string.Empty;
        Description = string.Empty;
        ImgUrl = string.Empty;
    }

    public int Id { get; init; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string ImgUrl { get; set; }

    public int AvailableQuantity { get; set; }

    public DateTimeOffset CreatedAt { get; init; }

    public DateTimeOffset? UpdatedAt { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

}
