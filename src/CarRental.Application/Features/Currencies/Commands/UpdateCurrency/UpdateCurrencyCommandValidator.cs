using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Currencies.Commands.UpdateCurrency;

/// <summary>
/// Validates the update Currency command.
/// </summary>
public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
{
    private readonly ICurrencyService _currencyService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCurrencyCommandValidator"/> class.
    /// </summary>
    public UpdateCurrencyCommandValidator(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
        ApplyCustomRules();
        ApplyRules();
    }

    private void ApplyCustomRules()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");

        RuleFor(x => x.ValueVsOneDollar)
            .GreaterThan(0).WithMessage("ValueVsOneDollar must be greater than 0.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Name)
            .MustAsync(async (request, Name, ct) =>
            {
                var exists = await _currencyService.ExistByNameAsync(Name, ct);
                return !exists;
            }).WithMessage("Name of Currency is already exist");
    }
}
