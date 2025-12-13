using APICatalogo.Context;
using APICatalogo.Entities;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

internal class ProductRepository : IProductRepository
{
    private readonly ApiCatalogoContext _context;

    public ProductRepository(ApiCatalogoContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return product;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync();
    }

    // Temporário
    public async Task UpdateAsync(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    // Temporário
    public async Task<Product?> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product is null)
            return null;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return product;
    }
}
