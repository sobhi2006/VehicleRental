using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.FeesBanks.Commands.CreateFeesBank;

/// <summary>
/// Validates the create FeesBank command.
/// </summary>
public class CreateFeesBankCommandValidator : AbstractValidator<CreateFeesBankCommand>
{
    private readonly IFeesBankService _feesBankService;
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFeesBankCommandValidator"/> class.
    /// </summary>
    public CreateFeesBankCommandValidator(IFeesBankService feesBankService)
    {
        _feesBankService = feesBankService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) =>
            {
                var exists = await _feesBankService.ExistsByNameAsync(name, cancellation);
                return !exists;
            })
            .WithMessage("A fees bank with the same Name is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than or equal to 0.");
    }
}
