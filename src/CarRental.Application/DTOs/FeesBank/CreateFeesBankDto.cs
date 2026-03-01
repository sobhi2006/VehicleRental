namespace CarRental.Application.DTOs.FeesBank;

/// <summary>
/// Data transfer object used to create FeesBank.
/// </summary>
public record CreateFeesBankDto
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; init; }
}
