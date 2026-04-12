using APICatalogo.Context;
using APICatalogo.WebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace APICatalogo.WebAPI.Repositories;

internal sealed class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiCatalogoContext _context;
    private IDbContextTransaction? _currentTransaction;

    public UnitOfWork(ApiCatalogoContext context)
    {
        _context = context;
    }

    public async Task BeginAsync(CancellationToken ct)
    {
        if (_currentTransaction != null) return;
        _currentTransaction = await _context.Database.BeginTransactionAsync(ct);
    }

    public async Task CommitAsync(CancellationToken ct)
    {
        try
        {
            await _context.SaveChangesAsync(ct);

            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync(ct);
            }
        }
        finally
        {
            DisposeTransaction();
        }
    }

    public async Task RollbackAsync(CancellationToken ct)
    {
        if (_currentTransaction != null)
        {
            try
            {
                await _currentTransaction.RollbackAsync(ct);
            }
            finally
            {
                DisposeTransaction();
            }
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct)
        => await _context.SaveChangesAsync(ct);

    private void DisposeTransaction()
    {
        _currentTransaction?.Dispose();
        _currentTransaction = null;
    }

    public void Dispose()
    {
        DisposeTransaction();
        _context.Dispose();
    }
}
