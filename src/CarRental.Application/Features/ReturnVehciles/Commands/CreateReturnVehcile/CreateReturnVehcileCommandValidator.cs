using CarRental.Application.Interfaces;
using FluentValidation;

namespace CarRental.Application.Features.ReturnVehicles.Commands.CreateReturnVehicle;

/// <summary>
/// Validates the create ReturnVehicle command.
/// </summary>
public class CreateReturnVehicleCommandValidator : AbstractValidator<CreateReturnVehicleCommand>
{
    private readonly IBookingVehicleService _bookingVehicleService;
    private readonly IReturnVehicleService _returnVehicleService;
    private readonly IDamageVehicleService _damageVehicleService;
    private readonly IFeesBankService _feesBankService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReturnVehicleCommandValidator"/> class.
    /// </summary>
    public CreateReturnVehicleCommandValidator(
        IBookingVehicleService bookingVehicleService,
        IReturnVehicleService returnVehicleService,
        IDamageVehicleService damageVehicleService,
        IFeesBankService feesBankService)
    {
        _bookingVehicleService = bookingVehicleService;
        _returnVehicleService = returnVehicleService;
        _damageVehicleService = damageVehicleService;
        _feesBankService = feesBankService;

        ApplyRules();
        ApplyCustomRules();
    }

    private void ApplyRules()
    {
        RuleFor(x => x.BookingId)
            .GreaterThan(0).WithMessage("BookingId must be greater than 0.");

        RuleFor(x => x.ActualReturnDate)
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("ActualReturnDate cannot be in the future.");

        RuleFor(x => x.MileageAfter)
            .GreaterThanOrEqualTo(0).WithMessage("MileageAfter must be greater than or equal to 0.");

        RuleFor(x => x.FeesBankIds)
            .NotNull().WithMessage("FeesBankIds cannot be null.")
            .Must(ids => ids is not null && ids.Count > 0).WithMessage("At least one FeesBankId is required to prepare return invoice.");

        RuleForEach(x => x.FeesBankIds)
            .GreaterThan(0).WithMessage("Each FeesBankId must be greater than 0.");

        RuleFor(x => x.FeesBankIds)
            .Must(ids => ids is not null && ids.Distinct().Count() == ids.Count)
            .WithMessage("FeesBankIds must not contain duplicates.");

        RuleFor(x => x.DamageId)
            .GreaterThan(0).WithMessage("DamageId must be greater than 0.")
            .When(x => x.DamageId.HasValue);
    }

    private void ApplyCustomRules()
    {
        RuleFor(x => x.BookingId)
            .MustAsync(async (bookingId, cancellationToken) =>
            {
                return await _bookingVehicleService.ExistsByIdAsync(bookingId, cancellationToken);
            })
            .WithMessage("Booking not found.")
            .MustAsync(async (bookingId, cancellationToken) =>
            {
                var alreadyReturned = await _returnVehicleService.ExistsByBookingIdAsync(bookingId, cancellationToken);
                return !alreadyReturned;
            })
            .WithMessage("A return record already exists for this booking.");

        RuleFor(x => x)
            .MustAsync(async (request, cancellationToken) =>
            {
                var bookingResult = await _bookingVehicleService.GetByIdAsync(request.BookingId, cancellationToken);
                return bookingResult.IsSuccess && bookingResult.Value is not null && request.ActualReturnDate >= bookingResult.Value.PickUpDate;
            })
            .WithMessage("ActualReturnDate cannot be before booking pick-up date.");

        RuleFor(x => x.DamageId)
            .MustAsync(async (request, damageId, cancellationToken) =>
            {
                if (!damageId.HasValue)
                {
                    return true;
                }

                var damageResult = await _damageVehicleService.GetByIdAsync(damageId.Value, cancellationToken);
                return damageResult.IsSuccess && damageResult.Value is not null && damageResult.Value.BookingId == request.BookingId;
            })
            .WithMessage("Damage record not found or does not belong to the booking.");

        RuleFor(x => x.FeesBankIds)
            .MustAsync(async (ids, cancellationToken) => ids is not null && await _feesBankService.ExistsByIdsAsync(ids, cancellationToken))
            .WithMessage("One or more FeesBank entries were not found.");

    }
}
