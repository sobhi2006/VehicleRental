using FluentValidation;

namespace CarRental.Application.Features.Payments.Commands.CreatePayment;

/// <summary>
/// Validates the create Payment command.
/// </summary>
public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePaymentCommandValidator"/> class.
    /// </summary>
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.CurrencyId)
            .GreaterThan(0).WithMessage("CurrencyId must be greater than 0.");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than or equal to 0.");

    }
}
