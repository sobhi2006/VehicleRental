using CarRental.Application.Interfaces;
using CarRental.Application.Services;
using FluentValidation;

namespace CarRental.Application.Features.Persons.Commands.CreatePerson;

/// <summary>
/// Validates the create Person command.
/// </summary>
public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly IPersonService _personService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePersonCommandValidator"/> class.
    /// </summary>
    public CreatePersonCommandValidator(IPersonService personService)
    {
        this._personService = personService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x.NationalNo)
            .MustAsync(async (nationalNo, cancellation) =>
            {
                var exists = await _personService.ExistsByNationalNoAsync(nationalNo, cancellation );
                return !exists;
            })
            .WithMessage("A person with the same NationalNo is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required.")
            .MaximumLength(500).WithMessage("FirstName must not exceed 500 characters.");

        RuleFor(x => x.MiddleName)
            .NotEmpty().WithMessage("MiddleName is required.")
            .MaximumLength(500).WithMessage("MiddleName must not exceed 500 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(500).WithMessage("LastName must not exceed 500 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(500).WithMessage("Email must not exceed 500 characters.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("PhoneNumber is required.")
            .MaximumLength(500).WithMessage("PhoneNumber must not exceed 500 characters.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.")
            .MaximumLength(500).WithMessage("Address must not exceed 500 characters.");
        
        RuleFor(x => x.NationalNo)
            .NotEmpty().WithMessage("NationalNo is required.")
            .MaximumLength(500).WithMessage("NationalNo must not exceed 500 characters.");
    }
}
