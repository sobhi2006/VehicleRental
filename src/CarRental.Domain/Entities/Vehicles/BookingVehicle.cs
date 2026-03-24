using CarRental.Domain.Common;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Entities.Vehicles;

/// <summary>
/// Represents the BookingVehicle entity.
/// </summary>
public class BookingVehicle : BaseEntity
{
    /// <summary>Gets or sets the DriverId.</summary>
    public long DriverId { get; set; }
    /// <summary>Gets or sets the VehicleId.</summary>
    public long VehicleId { get; set; }
    /// <summary>Gets or sets the Status.</summary>
    public StatusBooking Status { get; set; }
    /// <summary>Gets or sets the Notes.</summary>
    public string? Notes { get; set; }
    /// <summary>Gets or sets the PickUpDate.</summary>
    public DateTime PickUpDate { get; set; }
    /// <summary>Gets or sets the DropOffDate.</summary>
    public DateTime DropOffDate { get; set; }
    /// <summary>Gets or sets the related Driver.</summary>
    public virtual Driver? Driver { get; set; }
    /// <summary>Gets or sets the related Vehicle.</summary>
    public virtual Vehicle? Vehicle { get; set; }
    public virtual List<Payment> Payments { get; set; } = [];
}
