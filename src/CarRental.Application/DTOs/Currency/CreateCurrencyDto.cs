namespace CarRental.Application.DTOs.Currency;

/// <summary>
/// Data transfer object used to create Currency.
/// </summary>
public record CreateCurrencyDto
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the ValueVsOneDollar.</summary>
    public decimal ValueVsOneDollar { get; init; }
}
