using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Persons.Commands.UpdateNationalNo;

public class UpdateNationalNoCommandValidator : AbstractValidator<UpdateNationalNoCommand>
{
    private readonly IPersonService _personService;

    public UpdateNationalNoCommandValidator(IPersonService personService)
    {
        this._personService = personService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(p => p)
            .MustAsync(async (nationalNo, cancellation) =>
            {
                // Implement your logic to check if the NationalNo is unique
                // For example, you can call a service method to check if the NationalNo exists
                var exists = await _personService.ExistsByNationalNoExcludeSelfAsync(nationalNo, cancellation);
                return !exists;
            })
            .WithMessage("A person with the same NationalNo is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.NationalNo)
            .NotEmpty().WithMessage("NationalNo is required.")
            .MaximumLength(500).WithMessage("NationalNo must not exceed 500 characters.");
    }
}