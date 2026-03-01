using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.DamageVehicle;

namespace CarRental.Application.Features.DamageVehicles.Queries.GetDamageVehicleById;

/// <summary>
/// Query to get a DamageVehicle by id.
/// </summary>
/// <param name="Id">Identifier of the DamageVehicle.</param>
public record GetDamageVehicleByIdQuery(long Id) : IRequest<Result<DamageVehicleDto>>;
