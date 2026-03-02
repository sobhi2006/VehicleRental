using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;

namespace CarRental.Application.Features.ReturnVehicles.Queries.GetReturnVehicleById;

/// <summary>
/// Query to get a ReturnVehicle by id.
/// </summary>
/// <param name="Id">Identifier of the ReturnVehicle.</param>
public record GetReturnVehicleByIdQuery(long Id) : IRequest<Result<ReturnVehicleDto>>;
