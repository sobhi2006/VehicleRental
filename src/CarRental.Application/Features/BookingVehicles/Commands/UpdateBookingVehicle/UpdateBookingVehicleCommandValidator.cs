using System.Data;
using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.BookingVehicles.Commands.UpdateBookingVehicle;

/// <summary>
/// Validates the update BookingVehicle command.
/// </summary>
public class UpdateBookingVehicleCommandValidator : AbstractValidator<UpdateBookingVehicleCommand>
{
    private readonly IDriverService _driverService;
    private readonly IVehicleService _vehicleService;
    private readonly IBookingVehicleService _bookingVehicleService;
    private readonly IBlockListCustomerService _blockListCustomerService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBookingVehicleCommandValidator"/> class.
    /// </summary>
    public UpdateBookingVehicleCommandValidator(IDriverService driverService, IVehicleService vehicleService, IBookingVehicleService bookingVehicleService, IBlockListCustomerService blockListCustomerService)
    {
        _driverService = driverService;
        _vehicleService = vehicleService;
        _bookingVehicleService = bookingVehicleService;
        _blockListCustomerService = blockListCustomerService;
        ApplyRules();
        ApplyCustomValidations();
    }

    private void ApplyRules()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0.");

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

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status must be a valid enum value.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.");
    }

    private void ApplyCustomValidations()
    {
        RuleFor(b => b)
            .MustAsync(async (request, ct) =>
            {
                return await _bookingVehicleService.IsBookingVehicleExistAsync(request.Id, ct);
            }).WithMessage("The booking vehicle does not exist.")
            .MustAsync(async (request, ct) =>
            {
                return await _driverService.IsDriverLicenseValidAfterEndBookingAsync(request.DriverId, request.DropOffDate, ct);
            }).WithMessage("The driver does not have a valid license or he is not found.")
            .MustAsync(async (request, ct) =>
            {
                var vehicleAvailable = await _vehicleService.IsVehicleAvailableAsync(request.VehicleId, request.PickUpDate, request.DropOffDate, ct);
                return vehicleAvailable;
            }).WithMessage("The vehicle is not available.")
            .MustAsync(async (request, ct) =>
            {
                var vehicleBookingAvailable = await _bookingVehicleService.IsVehicleAvailableForBookingAsync(request.VehicleId, request.PickUpDate, request.DropOffDate, ct, request.Id);
                return vehicleBookingAvailable;
            }).WithMessage("The vehicle is already booked for the selected dates.");

        RuleFor(b => b.DriverId)
            .MustAsync(async (driverId, ct) =>
            {
                return !await _blockListCustomerService.IsDriverBlockedById(driverId, ct);
            }).WithMessage("Driver with the specified ID is blocked.");
    }
}
