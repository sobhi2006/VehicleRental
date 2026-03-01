namespace CarRental.Application.DTOs.Classification;

/// <summary>
/// Data transfer object used to update Classification.
/// </summary>
public record UpdateClassificationDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
}
