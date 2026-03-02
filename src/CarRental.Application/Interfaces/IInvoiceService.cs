using CarRental.Application.Common;
using CarRental.Domain.Entities;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Invoice operations.
/// </summary>
public interface IInvoiceService
{
    /// <summary>
    /// Creates a new Invoice.
    /// </summary>
    Task<Result<Invoice>> CreateAsync(Invoice entity, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Invoice.
    /// </summary>
    Task<Result<Invoice>> UpdateAsync(Invoice entity, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Invoice.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Invoice by id.
    /// </summary>
    Task<Result<Invoice>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Invoices with pagination.
    /// </summary>
    Task<Result<PaginatedList<Invoice>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
