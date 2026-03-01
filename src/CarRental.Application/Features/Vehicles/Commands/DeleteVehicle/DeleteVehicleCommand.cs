using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.Vehicles.Commands.DeleteVehicle;

/// <summary>
/// Command to delete a Vehicle.
/// </summary>
/// <param name="Id">Identifier of the Vehicle.</param>
public record DeleteVehicleCommand(long Id) : IRequest<Result>;
