using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Driver;

namespace CarRental.Application.Features.Drivers.Queries.GetDriverById;

/// <summary>
/// Query to get a Driver by id.
/// </summary>
/// <param name="Id">Identifier of the Driver.</param>
public record GetDriverByIdQuery(long Id) : IRequest<Result<DriverDto>>;
