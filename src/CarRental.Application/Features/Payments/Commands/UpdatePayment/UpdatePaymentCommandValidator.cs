using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Payments.Commands.UpdatePayment;

/// <summary>
/// Validates the update Payment command.
/// </summary>
public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    private readonly IBookingVehicleService _bookingVehicleService;
    private readonly ICurrencyService _currencyService;
    private readonly IPaymentService _paymentService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePaymentCommandValidator"/> class.
    /// </summary>
    public UpdatePaymentCommandValidator(IBookingVehicleService bookingVehicleService, ICurrencyService currencyService, IPaymentService paymentService)
    {
        _bookingVehicleService = bookingVehicleService;
        _currencyService = currencyService;
        _paymentService = paymentService;
        ApplyRules();
        ApplyCustomRules();
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.CurrencyId)
            .GreaterThan(0).WithMessage("CurrencyId must be greater than 0.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Type must be a valid enum value.");
    }

    private void ApplyCustomRules()
    {
        RuleFor(x => x.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                return await _paymentService.ExistsByIdAsync(id, cancellationToken);
            })
            .WithMessage("Payment not found.");

        RuleFor(x => x.BookingId)
            .MustAsync(async (bookingId, cancellationToken) =>
            {
                return await _bookingVehicleService.ExistsByIdAsync(bookingId, cancellationToken);
            })
            .WithMessage("Booking not found.");

        RuleFor(x => x.CurrencyId)
            .MustAsync(async (currencyId, cancellationToken) =>
            {
                return await _currencyService.ExistsByIdAsync(currencyId, cancellationToken);
            })
            .WithMessage("Currency not found.");
    }
}
