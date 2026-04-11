using APICatalogo.Entities;

namespace APICatalogo.WebAPI.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<Category> CreateAsync(Category category);
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> GetCategoryWithProductsAsync(int id);
    Task<List<Category>> GetAllListAsync();
    Task UpdateAsync(Category category);
    Task<Category?> DeleteAsync(int id);
    Task<bool> ExistsByNameAsync(string name);

}
