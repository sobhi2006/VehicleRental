namespace CarRental.Application.DTOs.Make;

/// <summary>
/// Data transfer object used to create Make.
/// </summary>
public record CreateMakeDto
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
}
