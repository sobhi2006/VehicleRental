using CarRental.Domain.Common;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Entities;

/// <summary>
/// Represents the BlockListCustomer entity.
/// </summary>
public class BlockListCustomer : BaseEntity
{
    /// <summary>Gets or sets the DriverId.</summary>
    public long DriverId { get; set; }
    /// <summary>Gets or sets the IsBlock.</summary>
    public bool IsBlock { get; set; }
    /// <summary>Gets or sets the Description.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Gets or sets the related Driver.</summary>
    public virtual Driver? Driver { get; set; }
}
