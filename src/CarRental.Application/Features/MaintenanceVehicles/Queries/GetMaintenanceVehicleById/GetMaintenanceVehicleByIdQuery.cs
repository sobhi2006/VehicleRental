using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;

namespace CarRental.Application.Features.MaintenanceVehicles.Queries.GetMaintenanceVehicleById;

/// <summary>
/// Query to get a MaintenanceVehicle by id.
/// </summary>
/// <param name="Id">Identifier of the MaintenanceVehicle.</param>
public record GetMaintenanceVehicleByIdQuery(long Id) : IRequest<Result<MaintenanceVehicleDto>>;
