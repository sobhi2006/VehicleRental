using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;

/// <summary>
/// Represents the InvoiceLine entity.
/// </summary>
public class InvoiceLine : BaseEntity
{
    /// <summary>Gets or sets the InvoiceId.</summary>
    public long InvoiceId { get; set; }
    /// <summary>Gets or sets the Description.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Gets or sets the Quantity.</summary>
    public decimal Quantity { get; set; }
    /// <summary>Gets or sets the UnitPrice.</summary>
    public decimal UnitPrice { get; set; }
    /// <summary>Gets or sets the LineTotal.</summary>
    public decimal LineTotal { get; set; }
    /// <summary>Gets or sets the related Invoice.</summary>
    public virtual Invoice? Invoice { get; set; }
}
