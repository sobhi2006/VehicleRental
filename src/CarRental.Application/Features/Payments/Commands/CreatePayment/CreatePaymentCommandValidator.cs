using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Payments.Commands.CreatePayment;

/// <summary>
/// Validates the create Payment command.
/// </summary>
public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    private readonly IBookingVehicleService _bookingVehicleService;
    private readonly ICurrencyService _currencyService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePaymentCommandValidator"/> class.
    /// </summary>
    public CreatePaymentCommandValidator(IBookingVehicleService bookingVehicleService, ICurrencyService currencyService)
    {
        _bookingVehicleService = bookingVehicleService;
        _currencyService = currencyService;
        ApplyRules();
        ApplyCustomRules();
    }

    private void ApplyRules()
    {
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
