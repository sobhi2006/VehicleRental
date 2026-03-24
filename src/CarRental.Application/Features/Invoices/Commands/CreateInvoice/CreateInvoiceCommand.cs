using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Invoice;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Invoices.Commands.CreateInvoice;

/// <summary>
/// Command to create a Invoice.
/// </summary>
public record CreateInvoiceCommand : IRequest<Result<InvoiceDto>>
{
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; init; }
    /// <summary>Gets or sets the IssueDate.</summary>
    public DateTime IssueDate { get; init; }
    /// <summary>Gets or sets the TotalAmount.</summary>
    public decimal TotalAmount { get; init; }
    /// <summary>Gets or sets the InvoiceLines.</summary>
    public List<InvoiceLine> InvoiceLines { get; init; } = [];
}
