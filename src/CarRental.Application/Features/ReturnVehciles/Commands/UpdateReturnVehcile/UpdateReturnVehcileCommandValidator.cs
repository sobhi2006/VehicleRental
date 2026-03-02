using FluentValidation;

namespace CarRental.Application.Features.ReturnVehicles.Commands.UpdateReturnVehicle;

/// <summary>
/// Validates the update ReturnVehicle command.
/// </summary>
public class UpdateReturnVehicleCommandValidator : AbstractValidator<UpdateReturnVehicleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateReturnVehicleCommandValidator"/> class.
    /// </summary>
    public UpdateReturnVehicleCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

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
