using FluentValidation;

namespace CarRental.Application.Features.DamageVehicles.Commands.CreateDamageVehicle;

/// <summary>
/// Validates the create DamageVehicle command.
/// </summary>
public class CreateDamageVehicleCommandValidator : AbstractValidator<CreateDamageVehicleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDamageVehicleCommandValidator"/> class.
    /// </summary>
    public CreateDamageVehicleCommandValidator()
    {
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
