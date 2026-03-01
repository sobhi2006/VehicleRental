using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Makes.Commands.UpdateMake;

/// <summary>
/// Validates the update Make command.
/// </summary>
public class UpdateMakeCommandValidator : AbstractValidator<UpdateMakeCommand>
{
    private readonly IMakeService _makeService;
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMakeCommandValidator"/> class.
    /// </summary>
    public UpdateMakeCommandValidator(IMakeService makeService)
    {
        _makeService = makeService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x)
            .MustAsync(async (request, cancellation) =>
            {
                var exists = await _makeService.ExistsByNameExcludeSelfAsync(request, cancellation);
                return !exists;
            })
            .WithMessage("A make with the same Name is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");
    }
}
