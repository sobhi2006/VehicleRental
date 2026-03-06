using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.BlockListCustomers.Commands.CreateBlockListCustomer;

/// <summary>
/// Validates the create BlockListCustomer command.
/// </summary>
public class CreateBlockListCustomerCommandValidator : AbstractValidator<CreateBlockListCustomerCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBlockListCustomerCommandValidator"/> class.
    /// </summary>
    public CreateBlockListCustomerCommandValidator(IDriverService driverService)
    {
        RuleFor(x => x.DriverId)
            .GreaterThan(0).WithMessage("DriverId must be greater than 0.")
            .MustAsync(async (driverId, cancellation) => 
            {
                var exist = await driverService.ExistsByIdAsync(driverId, cancellation);
                return exist;
            }).WithMessage("Driver with the specified DriverId does not exist.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

    }
}
