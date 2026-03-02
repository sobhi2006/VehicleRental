using FluentValidation;

namespace CarRental.Application.Features.ReturnVehicles.Commands.CreateReturnVehicle;

/// <summary>
/// Validates the create ReturnVehicle command.
/// </summary>
public class CreateReturnVehicleCommandValidator : AbstractValidator<CreateReturnVehicleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReturnVehicleCommandValidator"/> class.
    /// </summary>
    public CreateReturnVehicleCommandValidator()
    {
        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.MileageAfter)
            .GreaterThanOrEqualTo(0).WithMessage("MileageAfter must be greater than or equal to 0.");

        RuleFor(x => x.ExcessMileageFess)
            .GreaterThanOrEqualTo(0).WithMessage("ExcessMileageFess must be greater than or equal to 0.");

        RuleFor(x => x.DamageId)
            .GreaterThan(0).WithMessage("DamageId must be greater than 0.");

    }
}
