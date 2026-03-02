using FluentValidation;

namespace CarRental.Application.Features.Currencies.Commands.UpdateCurrency;

/// <summary>
/// Validates the update Currency command.
/// </summary>
public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCurrencyCommandValidator"/> class.
    /// </summary>
    public UpdateCurrencyCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");

        RuleFor(x => x.ValueVsOneDollar)
            .GreaterThanOrEqualTo(0).WithMessage("ValueVsOneDollar must be greater than or equal to 0.");

    }
}
