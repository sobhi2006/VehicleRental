using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.DamageVehicles.Commands.DeleteDamageVehicle;

/// <summary>
/// Command to delete a DamageVehicle.
/// </summary>
/// <param name="Id">Identifier of the DamageVehicle.</param>
public record DeleteDamageVehicleCommand(long Id) : IRequest<Result>;
