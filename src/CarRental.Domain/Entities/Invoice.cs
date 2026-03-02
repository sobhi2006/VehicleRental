using CarRental.Domain.Common;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities;

/// <summary>
/// Represents the Invoice entity.
/// </summary>
public class Invoice : BaseEntity
{
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; set; }
    /// <summary>Gets or sets the IssueDate.</summary>
    public DateTime IssueDate { get; set; }
    /// <summary>Gets or sets the TotalAmount.</summary>
    public decimal TotalAmount { get; set; }
    /// <summary>Gets or sets the PaidAmount.</summary>
    public decimal PaidAmount { get; set; }
    /// <summary>Gets or sets the Status.</summary>
    public InvoiceStatus Status { get; set; }
    /// <summary>Gets or sets the InvoiceLines.</summary>
    public List<InvoiceLine> InvoiceLines { get; set; } = [];
    /// <summary>Gets or sets the related BookingVehicle.</summary>
    public virtual BookingVehicle? BookingVehicle { get; set; }
}
