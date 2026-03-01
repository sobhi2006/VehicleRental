namespace CarRental.Application.DTOs.Make;

/// <summary>
/// Data transfer object used to update Make.
/// </summary>
public record UpdateMakeDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
}
