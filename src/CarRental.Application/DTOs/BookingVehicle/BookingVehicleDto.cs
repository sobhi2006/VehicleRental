using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs.BookingVehicle;

/// <summary>
/// Data transfer object for BookingVehicle.
/// </summary>
public record BookingVehicleDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the DriverId.</summary>
    public long DriverId { get; init; }
    /// <summary>Gets or sets the VehicleId.</summary>
    public long VehicleId { get; init; }
    /// <summary>Gets or sets the Status.</summary>
    public StatusBooking Status { get; init; }
    /// <summary>Gets or sets the Notes.</summary>
    public string? Notes { get; init; }
    /// <summary>Gets or sets the PickUpDate.</summary>
    public DateTime PickUpDate { get; init; }
    /// <summary>Gets or sets the DropOffDate.</summary>
    public DateTime DropOffDate { get; init; }
    /// <summary>Gets or sets the creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the last update timestamp.</summary>
    public DateTime? UpdatedAt { get; init; }
}
