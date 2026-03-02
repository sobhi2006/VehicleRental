namespace CarRental.Application.DTOs.Currency;

/// <summary>
/// Data transfer object used to update Currency.
/// </summary>
public record UpdateCurrencyDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the ValueVsOneDollar.</summary>
    public decimal ValueVsOneDollar { get; init; }
}
