using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.DamageVehicles.Commands.CreateDamageVehicle;

/// <summary>
/// Validates the create DamageVehicle command.
/// </summary>
public class CreateDamageVehicleCommandValidator : AbstractValidator<CreateDamageVehicleCommand>
{
    private readonly IVehicleService _vehicleService;
    private readonly IBookingVehicleService _bookingVehicleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDamageVehicleCommandValidator"/> class.
    /// </summary>
    public CreateDamageVehicleCommandValidator(IVehicleService vehicleService, IBookingVehicleService bookingVehicleService)
    {
        _vehicleService = vehicleService;
        _bookingVehicleService = bookingVehicleService;
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

        RuleFor(d => d.BookingId)
            .MustAsync(async (v, ct) =>
            {
                return await _bookingVehicleService.IsBookingVehicleExistAsync(v, ct);
            }).WithMessage("Vehicle not found");
    }

    private void ApplyValidation()
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

        RuleFor(x => x.DamageDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("DamageDate cannot be in the future.");

        RuleFor(x => x.Images)
            .Must(i => i != null)
            .ForEach(i => 
                i.Must(i => i is not null).WithMessage("Image cannot be null"))
            .WithMessage("Images cannot be null.");
    }
}
