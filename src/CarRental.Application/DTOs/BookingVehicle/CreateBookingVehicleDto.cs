using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs.BookingVehicle;

/// <summary>
/// Data transfer object used to create BookingVehicle.
/// </summary>
public record CreateBookingVehicleDto
{
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
}
