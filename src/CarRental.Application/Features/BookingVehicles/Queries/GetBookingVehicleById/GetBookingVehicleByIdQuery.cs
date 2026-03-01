using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BookingVehicle;

namespace CarRental.Application.Features.BookingVehicles.Queries.GetBookingVehicleById;

/// <summary>
/// Query to get a BookingVehicle by id.
/// </summary>
/// <param name="Id">Identifier of the BookingVehicle.</param>
public record GetBookingVehicleByIdQuery(long Id) : IRequest<Result<BookingVehicleDto>>;
