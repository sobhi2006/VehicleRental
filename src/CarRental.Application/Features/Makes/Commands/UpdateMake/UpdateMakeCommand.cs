using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Make;

namespace CarRental.Application.Features.Makes.Commands.UpdateMake;

/// <summary>
/// Command to update a Make.
/// </summary>
public record UpdateMakeCommand : IRequest<Result<MakeDto>>
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
}
