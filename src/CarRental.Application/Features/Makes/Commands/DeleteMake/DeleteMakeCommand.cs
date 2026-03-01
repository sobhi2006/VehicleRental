using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.Makes.Commands.DeleteMake;

/// <summary>
/// Command to delete a Make.
/// </summary>
/// <param name="Id">Identifier of the Make.</param>
public record DeleteMakeCommand(long Id) : IRequest<Result>;
