namespace CarRental.Application.DTOs.Classification;

/// <summary>
/// Data transfer object used to create Classification.
/// </summary>
public record CreateClassificationDto
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
}
