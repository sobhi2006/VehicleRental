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
    public CreateBlockListCustomerCommandValidator()
    {
        RuleFor(x => x.DriverId)
            .GreaterThan(0).WithMessage("DriverId must be greater than 0.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

    }
}
