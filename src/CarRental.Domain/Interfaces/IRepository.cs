using System.Linq.Expressions;
using CarRental.Domain.Common;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Generic repository contract for aggregate persistence.
/// </summary>
public interface IRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Gets an entity by its identifier.
    /// </summary>
    Task<T?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Gets all entities.
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Finds entities matching the predicate.
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    /// <summary>
    /// Checks whether any entity matches the predicate.
    /// </summary>
    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    /// <summary>
    /// Checks whether any entity matches the predicate, excluding the specified entity id.
    /// </summary>
    Task<bool> ExistsExcludeSelfAsync(long id, Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    /// <summary>
    /// Adds a new entity.
    /// </summary>
    Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Updates an existing entity.
    /// </summary>
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Deletes an existing entity.
    /// </summary>
    Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    /// <summary>
    /// Checks whether an entity exists for the given identifier.
    /// </summary>
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);
    /// <summary>
    /// Returns the total count of entities.
    /// </summary>
    Task<int> CountAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Gets a page of entities.
    /// </summary>
    Task<IReadOnlyList<T>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
