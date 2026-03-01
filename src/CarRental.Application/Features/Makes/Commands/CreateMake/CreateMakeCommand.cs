using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Make;

namespace CarRental.Application.Features.Makes.Commands.CreateMake;

/// <summary>
/// Command to create a Make.
/// </summary>
public record CreateMakeCommand : IRequest<Result<MakeDto>>
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
}
