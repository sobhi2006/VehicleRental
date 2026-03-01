using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.BookingVehicles.Commands.DeleteBookingVehicle;

/// <summary>
/// Command to delete a BookingVehicle.
/// </summary>
/// <param name="Id">Identifier of the BookingVehicle.</param>
public record DeleteBookingVehicleCommand(long Id) : IRequest<Result>;
