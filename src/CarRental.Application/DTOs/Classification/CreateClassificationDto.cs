namespace CarRental.Application.DTOs.Classification;

/// <summary>
/// Data transfer object used to create Classification.
/// </summary>
public record CreateClassificationDto
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the PaymentPerDay.</summary>
    public decimal PaymentPerDay { get; set; }
    /// <summary>Gets or sets the CostPerExKm.</summary>
    public decimal CostPerExKm { get; set; }
    /// <summary>Gets or sets the CostPerLateDay.</summary>
    public decimal CostPerLateDay { get; set; }
}
