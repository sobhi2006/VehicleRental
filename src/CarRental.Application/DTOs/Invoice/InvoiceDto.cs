using CarRental.Domain.Entities;
using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs.Invoice;

/// <summary>
/// Data transfer object for Invoice.
/// </summary>
public record InvoiceDto
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
    /// <summary>Gets or sets the creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the last update timestamp.</summary>
    public DateTime? UpdatedAt { get; init; }
}
