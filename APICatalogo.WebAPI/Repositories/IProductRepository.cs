using APICatalogo.Entities;

namespace APICatalogo.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    Task<Product?> GetByIdAsync(int id);
    Task<List<Product>> GetAllAsync();
    Task UpdateAsync(Product product);
    Task<Product?> DeleteAsync(int id);
}
