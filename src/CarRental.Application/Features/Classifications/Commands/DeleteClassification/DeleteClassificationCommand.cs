using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.Classifications.Commands.DeleteClassification;

/// <summary>
/// Command to delete a Classification.
/// </summary>
/// <param name="Id">Identifier of the Classification.</param>
public record DeleteClassificationCommand(long Id) : IRequest<Result>;
