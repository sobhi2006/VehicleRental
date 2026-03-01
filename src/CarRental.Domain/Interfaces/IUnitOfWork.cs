namespace CarRental.Domain.Interfaces;

/// <summary>
/// Unit of work contract for transactional boundaries.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Persists all pending changes.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Begins a database transaction.
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Commits the active transaction.
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Rolls back the active transaction.
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
