using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Drivers.Commands.UpdateDriver;

/// <summary>
/// Validates the update Driver command.
/// </summary>
public class UpdateDriverCommandValidator : AbstractValidator<UpdateDriverCommand>
{
    private readonly IDriverService _driverService;
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDriverCommandValidator"/> class.
    /// </summary>
    public UpdateDriverCommandValidator(IDriverService driverService)
    {
        _driverService = driverService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x)
            .MustAsync(async (request, cancellation) =>
            {
                var exists = await _driverService.ExistsByPersonIdExcludeSelfAsync(request, cancellation);
                return !exists;
            })
            .WithMessage("This person is already assigned as a driver.");

        RuleFor(x => x)
            .MustAsync(async (request, cancellation) =>
            {
                var exists = await _driverService.ExistsByDriverLicenseNumberExcludeSelfAsync(request, cancellation);
                return !exists;
            })
            .WithMessage("A driver with the same DriverLicenseNumber is invalid.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.PersonId)
            .GreaterThan(0).WithMessage("PersonId must be greater than 0.");

        RuleFor(x => x.DriverLicenseNumber)
            .NotEmpty().WithMessage("DriverLicenseNumber is required.")
            .MaximumLength(500).WithMessage("DriverLicenseNumber must not exceed 500 characters.");
    }
}
