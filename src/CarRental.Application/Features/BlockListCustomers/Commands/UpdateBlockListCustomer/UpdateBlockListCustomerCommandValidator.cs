using FluentValidation;

namespace CarRental.Application.Features.BlockListCustomers.Commands.UpdateBlockListCustomer;

/// <summary>
/// Validates the update BlockListCustomer command.
/// </summary>
public class UpdateBlockListCustomerCommandValidator : AbstractValidator<UpdateBlockListCustomerCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBlockListCustomerCommandValidator"/> class.
    /// </summary>
    public UpdateBlockListCustomerCommandValidator()
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
