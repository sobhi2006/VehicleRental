using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Drivers.Commands.CreateDriver;

/// <summary>
/// Validates the create Driver command.
/// </summary>
public class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
{
    private readonly IDriverService _driverService;
    private readonly IPersonService _personService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDriverCommandValidator"/> class.
    /// </summary>
    public CreateDriverCommandValidator(IDriverService driverService, IPersonService personService)
    {
        _driverService = driverService;
        _personService = personService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x.PersonId)
            .MustAsync(async (personId, cancellation) =>
            {
                var exists = await _personService.ExistsByIdAsync(personId, cancellation);
                return exists;
            })
            .WithMessage("This person not found.");

        RuleFor(x => x.PersonId)
            .MustAsync(async (personId, cancellation) =>
            {
                var exists = await _driverService.ExistsByPersonIdAsync(personId, cancellation);
                return !exists;
            })
            .WithMessage("This person is already assigned as a driver.");

        RuleFor(x => x.DriverLicenseNumber)
            .MustAsync(async (driverLicenseNumber, cancellation) =>
            {
                var exists = await _driverService.ExistsByDriverLicenseNumberAsync(driverLicenseNumber, cancellation);
                return !exists;
            })
            .WithMessage("A driver with the same DriverLicenseNumber is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.PersonId)
            .GreaterThan(0).WithMessage("PersonId must be greater than 0.");

        RuleFor(x => x.DriverLicenseNumber)
            .NotEmpty().WithMessage("DriverLicenseNumber is required.")
            .MaximumLength(500).WithMessage("DriverLicenseNumber must not exceed 500 characters.");

        RuleFor(x => x.DriverLicenseExpiryDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("DriverLicenseExpiryDate must be a future date.");
    }
}
