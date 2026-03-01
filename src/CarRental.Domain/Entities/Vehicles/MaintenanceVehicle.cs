using CarRental.Domain.Common;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the MaintenanceVehicle entity.
/// </summary>
public class MaintenanceVehicle : BaseEntity
{
    /// <summary>Gets or sets the VehicleId.</summary>
    public long VehicleId { get; set; }
    /// <summary>Gets or sets the Cost.</summary>
    public decimal Cost { get; set; }
    /// <summary>Gets or sets the StartDate.</summary>
    public DateTime StartDate { get; set; }
    /// <summary>Gets or sets the EndDate.</summary>
    public DateTime EndDate { get; set; }
    /// <summary>Gets or sets the Notes.</summary>
    public string Notes { get; set; } = string.Empty;
    /// <summary>Gets or sets the Status.</summary>
    public MaintenanceStatus Status { get; set; }
    /// <summary>Gets or sets the related Vehicle.</summary>
    public virtual Vehicle? Vehicle { get; set; }
}
