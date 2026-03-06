using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Classifications.Commands.CreateClassification;

/// <summary>
/// Validates the create Classification command.
/// </summary>
public class CreateClassificationCommandValidator : AbstractValidator<CreateClassificationCommand>
{
    private readonly IClassificationService _classificationService;
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateClassificationCommandValidator"/> class.
    /// </summary>
    public CreateClassificationCommandValidator(IClassificationService classificationService)
    {
        _classificationService = classificationService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) =>
            {
                var exists = await _classificationService.ExistsByNameAsync(name, cancellation);
                return !exists;
            })
            .WithMessage("A classification with the same Name is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");

        RuleFor(x => x.PaymentPerDay)
            .GreaterThan(0).WithMessage("PaymentPerDay must be greater than 0.");
    
        RuleFor(x => x.CostPerExKm)
            .GreaterThan(0).WithMessage("CostPerExKm must be greater than or equal to 0.");

        RuleFor(x => x.CostPerLateDay)
            .GreaterThan(0).WithMessage("CostPerLateDay must be greater than or equal to 0.");
    }
}
