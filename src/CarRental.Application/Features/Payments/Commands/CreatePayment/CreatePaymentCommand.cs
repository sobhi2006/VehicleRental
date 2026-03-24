using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;
using CarRental.Domain.Enums;

namespace CarRental.Application.Features.Payments.Commands.CreatePayment;

/// <summary>
/// Command to create a Payment.
/// </summary>
public record CreatePaymentCommand : IRequest<Result<PaymentDto>>
{
    /// <summary>Gets or sets the BookingId.</summary>
    public long BookingId { get; init; }
    /// <summary>Gets or sets the CurrencyId.</summary>
    public long CurrencyId { get; init; }
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; init; }
    /// <summary>Gets or sets the Type.</summary>
    public AmountType Type { get; init; }
}
