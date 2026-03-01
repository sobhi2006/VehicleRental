using CarRental.Domain.Common;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the Pricing entity.
/// </summary>
public class Pricing : BaseEntity
{
    /// <summary>Gets or sets the ClassificationId.</summary>
    public long ClassificationId { get; set; }
    /// <summary>Gets or sets the PaymentPerDay.</summary>
    public decimal PaymentPerDay { get; set; }
    /// <summary>Gets or sets the CostPerExKm.</summary>
    public decimal CostPerExKm { get; set; }
    /// <summary>Gets or sets the CostPerLateDay.</summary>
    public decimal CostPerLateDay { get; set; }
    /// <summary>Gets or sets the related Classification.</summary>
    public virtual Classification Classification { get; set; }
}
