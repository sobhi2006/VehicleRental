using Microsoft.EntityFrameworkCore.Storage;
using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure.Data;

/// <summary>
/// Unit of work implementation for EF Core.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction? _transaction;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Persists all pending changes.
    /// </summary>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Begins a database transaction.
    /// </summary>
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    /// <summary>
    /// Commits the active transaction.
    /// </summary>
    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <summary>
    /// Rolls back the active transaction.
    /// </summary>
    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <summary>
    /// Disposes the unit of work and underlying resources.
    /// </summary>
    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}
