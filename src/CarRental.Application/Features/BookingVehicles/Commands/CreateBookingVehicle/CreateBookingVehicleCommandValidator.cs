using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;

/// <summary>
/// Validates the create BookingVehicle command.
/// </summary>
public class CreateBookingVehicleCommandValidator : AbstractValidator<CreateBookingVehicleCommand>
{
    private readonly IDriverService _driverService;
    private readonly IVehicleService _vehicleService;
    private readonly IBookingVehicleService _bookingVehicleService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingVehicleCommandValidator"/> class.
    /// </summary>
    public CreateBookingVehicleCommandValidator(IDriverService driverService, IVehicleService vehicleService, IBookingVehicleService bookingVehicleService)
    {
        _driverService = driverService;
        _vehicleService = vehicleService;
        _bookingVehicleService = bookingVehicleService;

        ApplyRules();
        ApplyCustomValidations();
        
    }

    private void ApplyRules()
    {
        RuleFor(x => x.DriverId)
            .GreaterThan(0).WithMessage("DriverId must be greater than 0.");

        RuleFor(x => x.VehicleId)
            .GreaterThan(0).WithMessage("VehicleId must be greater than 0.");

        RuleFor(x => x.DropOffDate)
            .NotEmpty().WithMessage("DropOffDate is required.")
            .Must(date => date > DateTime.UtcNow).WithMessage("DropOffDate must be in the future.");

        RuleFor(bv => bv.PickUpDate)
            .NotEmpty().WithMessage("PickUpDate is required.")
            .Must(date => date > DateTime.UtcNow).WithMessage("PickUpDate must be in the future.")
            .LessThan(bv => bv.DropOffDate).WithMessage("PickUpDate must be before DropOffDate.");
    }

    private void ApplyCustomValidations()
    {
        RuleFor(b => b)
            .MustAsync(async (request, ct) =>
            {
                return await _driverService.IsDriverLicenseValidAsync(request.DriverId, ct);
            }).WithMessage("The driver does not have a valid license.")
            .MustAsync(async (request, ct) =>
            {
                var vehicleAvailable = await _vehicleService.IsVehicleAvailableAsync(request.VehicleId, request.PickUpDate, request.DropOffDate, ct);
                return vehicleAvailable;
            }).WithMessage("The vehicle is not available.")
            .MustAsync(async (request, ct) =>
            {
                var vehicleBookingAvailable = await _bookingVehicleService.IsVehicleAvailableForBookingAsync(request.VehicleId, request.PickUpDate, request.DropOffDate, ct);
                return vehicleBookingAvailable;
            }).WithMessage("The vehicle is already booked for the selected dates.");
    }
}
