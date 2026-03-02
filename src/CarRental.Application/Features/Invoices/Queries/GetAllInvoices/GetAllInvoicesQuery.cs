using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Invoice;

namespace CarRental.Application.Features.Invoices.Queries.GetAllInvoices;

/// <summary>
/// Query to get all Invoices with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllInvoicesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<InvoiceDto>>>;
