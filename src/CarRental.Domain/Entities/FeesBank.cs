using CarRental.Domain.Common;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Entities;

/// <summary>
/// Represents the FeesBank entity.
/// </summary>
public class FeesBank : BaseEntity
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; set; }
    /// <summary>Gets or sets the related return-fees mappings.</summary>
    public virtual List<ReturnVehicleFeesBank> ReturnVehicleFeesBanks { get; set; } = [];
}
