using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs.Payment;

/// <summary>
/// Data transfer object for Payment.
/// </summary>
public record PaymentDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; init; }
    /// <summary>Gets or sets the CurrencyId.</summary>
    public long CurrencyId { get; init; }
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; init; }
    /// <summary>Gets or sets the Type.</summary>
    public AmountType Type { get; init; }
    /// <summary>Gets or sets the Status.</summary>
    public PaymentStatus Status { get; init; }
    /// <summary>Gets or sets the creation timestamp.</summary>
    public DateTime CreatedAt { get; init; }
    /// <summary>Gets or sets the last update timestamp.</summary>
    public DateTime? UpdatedAt { get; init; }
}
