using APICatalogo.Context;
using APICatalogo.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.WebAPI.Repositories;

internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly ApiCatalogoContext _context;

    public Repository(ApiCatalogoContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(TEntity entity, CancellationToken ct = default)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken ct = default)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public async Task DeleteAsync(TEntity entity, CancellationToken ct = default)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Set<TEntity>().AsNoTracking().ToListAsync(ct);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, ct);
    }
}
