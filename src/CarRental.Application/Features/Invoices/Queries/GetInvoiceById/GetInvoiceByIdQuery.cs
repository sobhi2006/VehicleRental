using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Invoice;

namespace CarRental.Application.Features.Invoices.Queries.GetInvoiceById;

/// <summary>
/// Query to get a Invoice by id.
/// </summary>
/// <param name="Id">Identifier of the Invoice.</param>
public record GetInvoiceByIdQuery(long Id) : IRequest<Result<InvoiceDto>>;
