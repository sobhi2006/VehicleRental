using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.Vehicles.Commands.CreateVehicle;

/// <summary>
/// Validates the create Vehicle command.
/// </summary>
public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    private readonly IMakeService _makeService;
    private readonly IClassificationService _classificationService;
    private readonly IVehicleService _vehicleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateVehicleCommandValidator"/> class.
    /// </summary>
    public CreateVehicleCommandValidator(IMakeService makeService, IClassificationService classificationService, IVehicleService vehicleService)
    {
        _makeService = makeService;
        _classificationService = classificationService;
        _vehicleService = vehicleService;

        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyRules()
    {
        RuleFor(x => x.MakeId)
            .GreaterThan(0).WithMessage("MakeId must be greater than 0.");

        RuleFor(x => x.VIN)
            .NotEmpty().WithMessage("VIN is required.")
            .MaximumLength(500).WithMessage("VIN must not exceed 500 characters.");

        RuleFor(x => x.PlateNumber)
            .NotEmpty().WithMessage("PlateNumber is required.")
            .MaximumLength(500).WithMessage("PlateNumber must not exceed 500 characters.");

        RuleFor(x => x.CurrentMileage)
            .GreaterThanOrEqualTo(0).WithMessage("CurrentMileage must be greater than or equal to 0.");

        RuleFor(x => x.ClassificationId)
            .GreaterThan(0).WithMessage("ClassificationId must be greater than 0.");

        RuleFor(x => x.DoorsNumber)
            .GreaterThanOrEqualTo(0).WithMessage("DoorsNumber must be greater than or equal to 0.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value.");

        RuleFor(x => x.FuelType)
            .IsInEnum().WithMessage("Invalid fuel type value.");

        RuleFor(x => x.Transmission)
            .IsInEnum().WithMessage("Invalid transmission value.");
    }

    private void ApplyCustomValidations()
    {
        RuleFor(x => x.MakeId)
            .MustAsync(async (makeId, cancellation) =>
            {
                return await _makeService.ExistsAsync(makeId, cancellation);
            })
            .WithMessage("MakeId does not exist.");

        RuleFor(x => x.ClassificationId)
            .MustAsync(async (classificationId, cancellation) =>
            {
                return await _classificationService.ExistsAsync(classificationId, cancellation);
            })
            .WithMessage("ClassificationId does not exist.");

        RuleFor(x => x.VIN)
            .MustAsync(async (vin, cancellation) =>
            {
                return !await _vehicleService.ExistsByVinAsync(vin, cancellation);
            })
            .WithMessage("A vehicle with the same VIN already exists.");

        RuleFor(x => x.PlateNumber)
            .MustAsync(async (plateNumber, cancellation) =>
            {
                return !await _vehicleService.ExistsByPlateNumberAsync(plateNumber, cancellation);
            })
            .WithMessage("A vehicle with the same PlateNumber already exists.");
    }
}
