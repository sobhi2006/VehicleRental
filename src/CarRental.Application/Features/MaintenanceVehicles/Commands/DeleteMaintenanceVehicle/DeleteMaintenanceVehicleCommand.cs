using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.DeleteMaintenanceVehicle;

/// <summary>
/// Command to delete a MaintenanceVehicle.
/// </summary>
/// <param name="Id">Identifier of the MaintenanceVehicle.</param>
public record DeleteMaintenanceVehicleCommand(long Id) : IRequest<Result>;
