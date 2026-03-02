using CarRental.Domain.Entities;
using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs.Invoice;

/// <summary>
/// Data transfer object used to update Invoice.
/// </summary>
public record UpdateInvoiceDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; init; }
    /// <summary>Gets or sets the IssueDate.</summary>
    public DateTime IssueDate { get; init; }
    /// <summary>Gets or sets the TotalAmount.</summary>
    public decimal TotalAmount { get; init; }
    /// <summary>Gets or sets the PaidAmount.</summary>
    public decimal PaidAmount { get; init; }
    /// <summary>Gets or sets the Status.</summary>
    public InvoiceStatus Status { get; init; }
    /// <summary>Gets or sets the InvoiceLines.</summary>
    public List<InvoiceLine> InvoiceLines { get; init; } = [];
}
