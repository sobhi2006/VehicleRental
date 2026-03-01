using CarRental.Domain.Common;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the Classification entity.
/// </summary>
public class Classification : BaseEntity
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the Payment for Classification.
    /// </summary>
    public virtual Pricing Pricing { get; set; }
}
