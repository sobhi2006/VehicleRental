using CarRental.Application.Common;
using CarRental.Domain.Entities;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for InvoiceLine operations.
/// </summary>
public interface IInvoiceLineService
{
    /// <summary>
    /// Creates a new InvoiceLine.
    /// </summary>
    Task<Result<InvoiceLine>> CreateAsync(InvoiceLine request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing InvoiceLine.
    /// </summary>
    Task<Result<InvoiceLine>> UpdateAsync(InvoiceLine request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing InvoiceLine.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a InvoiceLine by id.
    /// </summary>
    Task<Result<InvoiceLine>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all InvoiceLines with pagination.
    /// </summary>
    Task<Result<PaginatedList<InvoiceLine>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
