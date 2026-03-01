using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Classification;

namespace CarRental.Application.Features.Classifications.Commands.CreateClassification;

/// <summary>
/// Command to create a Classification.
/// </summary>
public record CreateClassificationCommand : IRequest<Result<ClassificationDto>>
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
}
