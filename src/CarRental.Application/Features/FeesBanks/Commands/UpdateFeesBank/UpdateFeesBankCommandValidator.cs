using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.FeesBanks.Commands.UpdateFeesBank;

/// <summary>
/// Validates the update FeesBank command.
/// </summary>
public class UpdateFeesBankCommandValidator : AbstractValidator<UpdateFeesBankCommand>
{
    private readonly IFeesBankService _feesBankService;
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFeesBankCommandValidator"/> class.
    /// </summary>
    public UpdateFeesBankCommandValidator(IFeesBankService feesBankService)
    {
        _feesBankService = feesBankService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x)
            .MustAsync(async (request, cancellation) =>
            {
                var exists = await _feesBankService.ExistsByNameExcludeSelfAsync(request, cancellation);
                return !exists;
            })
            .WithMessage("A fees bank with the same Name is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0).WithMessage("Amount must be greater than or equal to 0.");
    }
}
