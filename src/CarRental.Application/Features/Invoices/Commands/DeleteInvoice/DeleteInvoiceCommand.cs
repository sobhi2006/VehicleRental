using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.Invoices.Commands.DeleteInvoice;

/// <summary>
/// Command to delete a Invoice.
/// </summary>
/// <param name="Id">Identifier of the Invoice.</param>
public record DeleteInvoiceCommand(long Id) : IRequest<Result>;
