using FluentValidation;

namespace CarRental.Application.Features.Payments.Commands.UpdatePayment;

/// <summary>
/// Validates the update Payment command.
/// </summary>
public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePaymentCommandValidator"/> class.
    /// </summary>
    public UpdatePaymentCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.CurrencyId)
            .GreaterThan(0).WithMessage("CurrencyId must be greater than 0.");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than or equal to 0.");

    }
}
