using FluentValidation;

namespace CarRental.Application.Features.Currencies.Commands.CreateCurrency;

/// <summary>
/// Validates the create Currency command.
/// </summary>
public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCurrencyCommandValidator"/> class.
    /// </summary>
    public CreateCurrencyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");

        RuleFor(x => x.ValueVsOneDollar)
            .GreaterThanOrEqualTo(0).WithMessage("ValueVsOneDollar must be greater than or equal to 0.");

    }
}
