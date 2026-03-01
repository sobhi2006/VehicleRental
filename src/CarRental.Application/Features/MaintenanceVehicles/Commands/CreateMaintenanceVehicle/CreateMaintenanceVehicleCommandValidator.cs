using FluentValidation;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;

/// <summary>
/// Validates the create MaintenanceVehicle command.
/// </summary>
public class CreateMaintenanceVehicleCommandValidator : AbstractValidator<CreateMaintenanceVehicleCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMaintenanceVehicleCommandValidator"/> class.
    /// </summary>
    public CreateMaintenanceVehicleCommandValidator()
    {
        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("VehicleId must be greater than 0.");

        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).WithMessage("Cost must be greater than or equal to 0.");

        RuleFor(x => x.Notes)
            .NotEmpty().WithMessage("Notes is required.")
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.");

    }
}
