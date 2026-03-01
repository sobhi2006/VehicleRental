namespace CarRental.Application.DTOs.FeesBank;

/// <summary>
/// Data transfer object used to update FeesBank.
/// </summary>
public record UpdateFeesBankDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; init; }
}
