using FluentValidation;

namespace CarRental.Application.Features.DamageVehicles.Commands.UpdateDamageVehicle;

/// <summary>
/// Validates the update DamageVehicle command.
/// </summary>
public class UpdateDamageVehicleCommandValidator : AbstractValidator<UpdateDamageVehicleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDamageVehicleCommandValidator"/> class.
    /// </summary>
    public UpdateDamageVehicleCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("VehicleId must be greater than 0.");

        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.RepairCost)
            .GreaterThanOrEqualTo(0).WithMessage("RepairCost must be greater than or equal to 0.");

    }
}
