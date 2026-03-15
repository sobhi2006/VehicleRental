using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.DamageVehicles.Commands.UpdateDamageVehicle;

/// <summary>
/// Validates the update DamageVehicle command.
/// </summary>
public class UpdateDamageVehicleCommandValidator : AbstractValidator<UpdateDamageVehicleCommand>
{
    private readonly IVehicleService _vehicleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDamageVehicleCommandValidator"/> class.
    /// </summary>
    public UpdateDamageVehicleCommandValidator(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
        ApplyValidation();
        ApplyCustomValidation();
    }

    private void ApplyCustomValidation()
    {
        RuleFor(d => d.VehicleId)
            .MustAsync(async (v, ct) =>
            {
                return await _vehicleService.IsExistById(v, ct);
            }).WithMessage("Vehicle not found");
    }

    private void ApplyValidation()
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

        RuleFor(x => x.DamageDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DamageDate cannot be in the future.");

        RuleFor(x => x.Images)
            .Must(i => i != null)
            .ForEach(i => 
                i.Must(i => i is not null).WithMessage("Image cannot be null"))
            .WithMessage("Images cannot be null.");

        RuleFor(x => x.ImageIDsToRemove)
            .Must(i => i != null)
            .ForEach(i => 
                i.Must(i => i > 0).WithMessage("Image ID to remove must be greater than 0"))
            .WithMessage("ImageIDsToRemove cannot be null.");
    }
}
