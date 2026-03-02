using CarRental.Domain.Enums;

namespace CarRental.Application.DTOs.Payment;

/// <summary>
/// Data transfer object used to update Payment.
/// </summary>
public record UpdatePaymentDto
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
}
