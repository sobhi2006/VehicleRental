using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Makes.Commands.CreateMake;

/// <summary>
/// Validates the create Make command.
/// </summary>
public class CreateMakeCommandValidator : AbstractValidator<CreateMakeCommand>
{
    private readonly IMakeService _makeService;
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMakeCommandValidator"/> class.
    /// </summary>
    public CreateMakeCommandValidator(IMakeService makeService)
    {
        _makeService = makeService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x.Name)
            .MustAsync(async (name, cancellation) =>
            {
                var exists = await _makeService.ExistsByNameAsync(name, cancellation);
                return !exists;
            })
            .WithMessage("A make with the same Name is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(500).WithMessage("Name must not exceed 500 characters.");
    }
}
