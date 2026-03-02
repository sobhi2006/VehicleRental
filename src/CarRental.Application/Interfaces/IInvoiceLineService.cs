using CarRental.Application.Common;
using CarRental.Application.DTOs.InvoiceLine;
using CarRental.Application.Features.InvoiceLines.Commands.CreateInvoiceLine;
using CarRental.Application.Features.InvoiceLines.Commands.UpdateInvoiceLine;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for InvoiceLine operations.
/// </summary>
public interface IInvoiceLineService
{
    /// <summary>
    /// Creates a new InvoiceLine.
    /// </summary>
    Task<Result<InvoiceLineDto>> CreateAsync(CreateInvoiceLineCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing InvoiceLine.
    /// </summary>
    Task<Result<InvoiceLineDto>> UpdateAsync(UpdateInvoiceLineCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing InvoiceLine.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a InvoiceLine by id.
    /// </summary>
    Task<Result<InvoiceLineDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all InvoiceLines with pagination.
    /// </summary>
    Task<Result<PaginatedList<InvoiceLineDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
