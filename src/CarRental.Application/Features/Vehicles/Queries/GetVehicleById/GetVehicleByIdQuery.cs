using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;

namespace CarRental.Application.Features.Vehicles.Queries.GetVehicleById;

/// <summary>
/// Query to get a Vehicle by id.
/// </summary>
/// <param name="Id">Identifier of the Vehicle.</param>
public record GetVehicleByIdQuery(long Id) : IRequest<Result<VehicleDto>>;
