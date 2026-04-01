using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Currencies.Commands.CreateCurrency;

/// <summary>
/// Validates the create Currency command.
/// </summary>
public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
{
    private readonly ICurrencyService _currencyService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCurrencyCommandValidator"/> class.
    /// </summary>
    public CreateCurrencyCommandValidator(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
        ApplyRules();
        ApplyCustomRules();
    }

    private void ApplyCustomRules()
    {
        RuleFor(x => x.Name)
            .MustAsync(async (Name, ct) =>
            {
                var exists = await _currencyService.ExistByNameAsync(Name, ct);
                return !exists;
            }).WithMessage("Name of Currency is already exist");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");

        RuleFor(x => x.ValueVsOneDollar)
            .GreaterThan(0).WithMessage("ValueVsOneDollar must be greater than 0.");
    }
}
