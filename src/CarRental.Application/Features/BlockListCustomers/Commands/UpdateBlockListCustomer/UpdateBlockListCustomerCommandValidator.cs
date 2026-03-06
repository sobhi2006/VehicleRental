using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.BlockListCustomers.Commands.UpdateBlockListCustomer;

/// <summary>
/// Validates the update BlockListCustomer command.
/// </summary>
public class UpdateBlockListCustomerCommandValidator : AbstractValidator<UpdateBlockListCustomerCommand>
{
    private readonly IDriverService _driverService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBlockListCustomerCommandValidator"/> class.
    /// </summary>
    public UpdateBlockListCustomerCommandValidator(IDriverService driverService)
    {
        _driverService = driverService;
        ApplyRules();
        ApplyCostumeRules();
    }

    private void ApplyCostumeRules()
    {
        RuleFor(x => x.DriverId)
            .MustAsync(async (driverId, cancellation) =>
            {
                var exist = await _driverService.ExistsByIdAsync(driverId, cancellation);
                return exist;
            }).WithMessage("Driver with the specified DriverId does not exist.");
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.DriverId)
            .GreaterThan(0).WithMessage("DriverId must be greater than 0.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
    }
}
