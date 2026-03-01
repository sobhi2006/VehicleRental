using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BookingVehicle;
using CarRental.Domain.Enums;

namespace CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;

/// <summary>
/// Command to create a BookingVehicle.
/// </summary>
public record CreateBookingVehicleCommand : IRequest<Result<BookingVehicleDto>>
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
