using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.Drivers.Commands.DeleteDriver;

/// <summary>
/// Command to delete a Driver.
/// </summary>
/// <param name="Id">Identifier of the Driver.</param>
public record DeleteDriverCommand(long Id) : IRequest<Result>;
