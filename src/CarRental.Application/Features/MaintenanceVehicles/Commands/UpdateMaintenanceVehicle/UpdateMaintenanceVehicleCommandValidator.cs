using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.UpdateMaintenanceVehicle;

/// <summary>
/// Validates the update MaintenanceVehicle command.
/// </summary>
public class UpdateMaintenanceVehicleCommandValidator : AbstractValidator<UpdateMaintenanceVehicleCommand>
{
    private readonly IVehicleService _vehicleService;
    private readonly IMaintenanceVehicleService _maintenanceVehicleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMaintenanceVehicleCommandValidator"/> class.
    /// </summary>
    public UpdateMaintenanceVehicleCommandValidator(IVehicleService vehicleService, IMaintenanceVehicleService maintenanceVehicleService)
    {
        _vehicleService = vehicleService;
        _maintenanceVehicleService = maintenanceVehicleService;
        ApplyValidation();
        ApplyCustomValidations();
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x.VehicleId)
            .MustAsync(async (vehicleId, cancellationToken) =>
            {
                var vehicleExists = await _vehicleService.IsExistById(vehicleId, cancellationToken);
                return vehicleExists;
            })
            .WithMessage("Vehicle not found.");
        
        RuleFor(x => x)
            .MustAsync(async (request, cancellationToken) =>
            {
                var isUnderMaintenance = await _maintenanceVehicleService.IsUnderMaintenanceAsync(
                                                                request.VehicleId, 
                                                                  request.StartDate, 
                                                                    request.EndDate, 
                                                                           cancellationToken);
                return !isUnderMaintenance;
            })
            .WithMessage("Vehicle is currently under maintenance.");
            
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value.");
    }

    private void ApplyValidation()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("VehicleId must be greater than 0.");

        RuleFor(x => x.Cost)
            .GreaterThanOrEqualTo(0).WithMessage("Cost must be greater than or equal to 0.");

        RuleFor(x => x.Notes)
            .NotEmpty().WithMessage("Notes is required.")
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value.");
    }
}
