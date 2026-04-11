using APICatalogo.Context;
using APICatalogo.Entities;
using APICatalogo.WebAPI.Repositories;
using APICatalogo.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

internal class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApiCatalogoContext context)
        : base(context)
    {
    }

    public async Task<Category?> GetCategoryWithProductsAsync(int id, CancellationToken ct)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public async Task<bool> ExistsByNameAsync(string name, CancellationToken ct)
    {
        return await _context.Categories
            .AsNoTracking()
            .AnyAsync(category => category.Name == name, ct);
    }
}
