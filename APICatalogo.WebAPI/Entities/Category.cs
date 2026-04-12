namespace APICatalogo.Entities;

public sealed class Category
{
    public Category(string name, string imgUrl)
    {
        Name = name;
        ImgUrl = imgUrl;
        CreatedAt = DateTimeOffset.UtcNow;
        Products = [];
    }
    private Category()
    {
        Name = string.Empty;
        ImgUrl = string.Empty;
    }

    public int Id { get; init; }
    public string Name { get; set; }
    public string ImgUrl { get; set; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public ICollection<Product> Products { get; set; } = [];
}
