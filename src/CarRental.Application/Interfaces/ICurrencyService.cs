using CarRental.Application.Common;
using CarRental.Domain.Entities;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Currency operations.
/// </summary>
public interface ICurrencyService
{
    /// <summary>
    /// Creates a new Currency.
    /// </summary>
    Task<Result<Currency>> CreateAsync(Currency request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Currency.
    /// </summary>
    Task<Result<Currency>> UpdateAsync(Currency request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Currency.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Currency by id.
    /// </summary>
    Task<Result<Currency>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Currencies with pagination.
    /// </summary>
    Task<Result<PaginatedList<Currency>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> ExistByNameAsync(string name, CancellationToken ct);
    Task<bool> ExistByNameExcludeSelfAsync(long id, string name, CancellationToken ct);
    Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken);
}
