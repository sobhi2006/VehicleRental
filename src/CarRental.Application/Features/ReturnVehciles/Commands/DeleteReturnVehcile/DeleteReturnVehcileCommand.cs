using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.ReturnVehicles.Commands.DeleteReturnVehicle;

/// <summary>
/// Command to delete a ReturnVehicle.
/// </summary>
/// <param name="Id">Identifier of the ReturnVehicle.</param>
public record DeleteReturnVehicleCommand(long Id) : IRequest<Result>;
