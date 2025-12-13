using APICatalogo.Context;
using APICatalogo.Entities;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

internal class CategoryRepository : ICategoryRepository
{
    private readonly ApiCatalogoContext _context;

    public CategoryRepository(ApiCatalogoContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category?> GetCategoryWithProductsAsync(int id)
    {
        return await _context.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Category>> GetAllListAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .ToListAsync();
    }

    // Temporário
    public async Task UpdateAsync(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();

    }

    // Temporário
    public async Task<Category?> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is null)
            return null;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return category;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Categories
            .AsNoTracking()
            .AnyAsync(category => category.Name == name);
    }
}
