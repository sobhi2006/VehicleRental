using CarRental.Domain.Common;
using CarRental.Domain.Entities.ImageEntities;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the DamageVehicle entity.
/// </summary>
public class DamageVehicle : BaseEntity
{
    /// <summary>Gets or sets the VehicleId.</summary>
    public long VehicleId { get; set; }
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; set; }
    /// <summary>Gets or sets the Severity.</summary>
    public SeverityStatus Severity { get; set; }
    /// <summary>Gets or sets the Description.</summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>Gets or sets the Images.</summary>
    public List<DamageVehicleImage> Images { get; set; } = [];
    /// <summary>Gets or sets the RepairCost.</summary>
    public decimal RepairCost { get; set; }
    /// <summary>Gets or sets the DamageDate.</summary>
    public DateTime DamageDate { get; set; }
    /// <summary>Gets or sets the related Vehicle.</summary>
    public virtual Vehicle Vehicle { get; set; }
    /// <summary>Gets or sets the related BookingVehicle.</summary>
    public virtual BookingVehicle BookingVehicle { get; set; }
}
