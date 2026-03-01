namespace CarRental.Application.DTOs.Classification;

/// <summary>
/// Data transfer object for Classification.
/// </summary>
public record ClassificationDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the PaymentPerDay.</summary>
    public decimal PaymentPerDay { get; set; }
    /// <summary>Gets or sets the CostPerExKm.</summary>
    public decimal CostPerExKm { get; set; }
    /// <summary>Gets or sets the CostPerLateDay.</summary>
    public decimal CostPerLateDay { get; set; }
    /// <summary>Gets or sets the creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the last update timestamp.</summary>
    public DateTime? UpdatedAt { get; init; }
}
