using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for ReturnVehicle.
/// </summary>
public class ReturnVehicleService : IReturnVehicleService
{
    private const string ReturnFeeLinePrefix = "RETURN_FEE:";
    private DateTime _dropOffDateBooking;
    private DateTime _pickUpDateBooking;
    private decimal _MileageBefore;
    private decimal _MileageAfter;

    private readonly IReturnVehicleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookingVehicleService _bookingVehicleService;
    private readonly IDamageVehicleService _damageVehicleService;
    private readonly IFeesBankService _feesBankService;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPricingRepository _pricingRepository;
    private readonly IVehicleRepository _vehicleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReturnVehicleService"/> class.
    /// </summary>
    public ReturnVehicleService(
        IReturnVehicleRepository repository,
        IUnitOfWork unitOfWork,
        IBookingVehicleService bookingVehicleService,
        IDamageVehicleService damageVehicleService,
        IFeesBankService feesBankService,
        IInvoiceRepository invoiceRepository,
        IPaymentRepository paymentRepository,
        IPricingRepository pricingRepository,
        IVehicleRepository vehicleRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _bookingVehicleService = bookingVehicleService;
        _damageVehicleService = damageVehicleService;
        _feesBankService = feesBankService;
        _invoiceRepository = invoiceRepository;
        _paymentRepository = paymentRepository;
        _pricingRepository = pricingRepository;
        _vehicleRepository = vehicleRepository;
    }

    /// <summary>
    /// Creates a new ReturnVehicle.
    /// </summary>
    public async Task<Result<ReturnVehicle>> CreateAsync(ReturnVehicle request, IReadOnlyCollection<long> feesBankIds, CancellationToken cancellationToken)
    {
        var alreadyExists = await _repository.ExistsByBookingIdAsync(request.BookingId, cancellationToken);
        if (alreadyExists)
        {
            return Result<ReturnVehicle>.Failure("A return record already exists for this booking.");
        }

        var validationError = await ValidateReturnRequestAsync(request, feesBankIds, cancellationToken);
        if (validationError is not null)
        {
            return Result<ReturnVehicle>.Failure(validationError);
        }

        AttachFeesBanks(request, feesBankIds);
        _MileageAfter = request.MileageAfter;

        var feesBanks = await _feesBankService.GetByIdsAsync(feesBankIds, cancellationToken);
        await UpsertReturnInvoiceAsync(request.BookingId, request.ActualReturnDate, feesBanks, cancellationToken);

        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing ReturnVehicle.
    /// </summary>
    public async Task<Result<ReturnVehicle>> UpdateAsync(ReturnVehicle request, IReadOnlyCollection<long> feesBankIds, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdWithDetailsAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<ReturnVehicle>.Failure("ReturnVehicle not found.");
        }

        var validationError = await ValidateReturnRequestAsync(request, feesBankIds, cancellationToken);
        if (validationError is not null)
        {
            return Result<ReturnVehicle>.Failure(validationError);
        }

        var alreadyExists = await _repository.ExistsByBookingIdExcludeSelfAsync(request.Id, request.BookingId, cancellationToken);
        if (alreadyExists)
        {
            return Result<ReturnVehicle>.Failure("A return record already exists for this booking.");
        }

        entity.BookingId = request.BookingId;
        entity.ConditionNotes = request.ConditionNotes;
        entity.ActualReturnDate = request.ActualReturnDate;
        entity.MileageAfter = request.MileageAfter;
        entity.DamageId = request.DamageId;

        entity.ReturnVehicleFeesBanks.Clear();
        AttachFeesBanks(entity, feesBankIds);

        _MileageAfter = request.MileageAfter;

        var feesBanks = await _feesBankService.GetByIdsAsync(feesBankIds, cancellationToken);
        await UpsertReturnInvoiceAsync(entity.BookingId, entity.ActualReturnDate, feesBanks, cancellationToken);

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing ReturnVehicle.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("ReturnVehicle not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a ReturnVehicle by id.
    /// </summary>
    public async Task<Result<ReturnVehicle>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<ReturnVehicle>.Failure("ReturnVehicle not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all ReturnVehicles with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<ReturnVehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<ReturnVehicle>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<ReturnVehicle>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<ReturnVehicle>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<ReturnVehicle>>.Success(paginated);
    }

    public Task<bool> ExistsByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
    {
        return _repository.ExistsByBookingIdAsync(bookingId, cancellationToken);
    }

    public Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(r => r.Id == id, cancellationToken);
    }

    public Task<bool> ExistsByBookingIdExcludeSelfAsync(long id, long bookingId, CancellationToken cancellationToken)
    {
        return _repository.ExistsByBookingIdExcludeSelfAsync(id, bookingId, cancellationToken);
    }

    private async Task<string?> ValidateReturnRequestAsync(ReturnVehicle request, IReadOnlyCollection<long> feesBankIds, CancellationToken cancellationToken)
    {
        var bookingResult = await _bookingVehicleService.GetByIdAsync(request.BookingId, cancellationToken);
        if (bookingResult.IsFailure || bookingResult.Value is null)
        {
            return "Booking not found.";
        }
        _dropOffDateBooking = bookingResult.Value.DropOffDate;
        _pickUpDateBooking = bookingResult.Value.PickUpDate;

        if (request.ActualReturnDate < bookingResult.Value.PickUpDate)
        {
            return "ActualReturnDate cannot be before booking pick-up date.";
        }

        if (request.DamageId.HasValue)
        {
            var damageResult = await _damageVehicleService.GetByIdAsync(request.DamageId.Value, cancellationToken);
            if (damageResult.IsFailure || damageResult.Value is null)
            {
                return "DamageVehicle not found.";
            }

            if (damageResult.Value.BookingId != request.BookingId)
            {
                return "Damage record must belong to the same booking.";
            }
        }

        var feesExist = await _feesBankService.ExistsByIdsAsync(feesBankIds, cancellationToken);
        if (!feesExist)
        {
            return "One or more FeesBank entries were not found.";
        }

        return null;
    }

    private static void AttachFeesBanks(ReturnVehicle returnVehicle, IReadOnlyCollection<long> feesBankIds)
    {
        foreach (var feesBankId in feesBankIds.Distinct())
        {
            returnVehicle.ReturnVehicleFeesBanks.Add(new ReturnVehicleFeesBank
            {
                FeesBankId = feesBankId
            });
        }
    }

    private async Task UpsertReturnInvoiceAsync(long bookingId, DateTime issueDate, IReadOnlyCollection<FeesBank> feesBanks, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetInvoiceByBookingIdAsync(bookingId, cancellationToken);

        var returnLines = feesBanks.Select(feesBank => new InvoiceLine
        {
            Description = $"{ReturnFeeLinePrefix}{feesBank.Id}:{feesBank.Name}",
            Quantity = 1,
            UnitPrice = feesBank.Amount,
        }).ToList();

        var pricing = await _pricingRepository.GetPricingByBookingIdAsync(bookingId, cancellationToken);
        if (pricing != null)
        {
            var ReturnDateLate = (DateTime.UtcNow - _dropOffDateBooking).Days;
            var amountReturnLate =  ReturnDateLate * pricing.CostPerLateDay;

            var ReturnDate = (_dropOffDateBooking - _pickUpDateBooking).Days;
            var amountReturn =  ReturnDate == 0 ? pricing.PaymentPerDay : ReturnDate * pricing.PaymentPerDay;

            var amountExcessMileage = (_MileageAfter - await _bookingVehicleService.GetCurrentMilageByBookingVehicleIdAsync(bookingId, cancellationToken)) * pricing.CostPerExKm;
            if (amountExcessMileage > 0)
            {
                returnLines.Add(new InvoiceLine
                {
                    Description = $"{ReturnFeeLinePrefix}:EXCESS_MILEAGE_FEE",
                    Quantity = 1,
                    UnitPrice = amountExcessMileage,
                });
            }

            if (amountReturnLate > 0)
            {
                returnLines.Add(new InvoiceLine
                {
                    Description = $"{ReturnFeeLinePrefix}:LATE_RETURN_FEE",
                    Quantity = 1,
                    UnitPrice = amountReturnLate,
                });
            }
            if(amountReturn > 0)
            {
                returnLines.Add(new InvoiceLine
                {
                    Description = $"RETURN VEHICLE",
                    Quantity = 1,
                    UnitPrice = amountReturn,
                });
            }
        }

        var netCompletedAmount = await _paymentRepository.GetNetCompletedAmountByBookingIdAsync(bookingId, cancellationToken);
        var totalAmount = returnLines.Sum(line => line.LineTotal);

        if (invoice is null)
        {
            var newInvoice = new Invoice
            {
                BookingId = bookingId,
                IssueDate = issueDate,
                InvoiceLines = returnLines,
                TotalAmount = totalAmount,
                PaidAmount = netCompletedAmount,
                Status = Math.Min(netCompletedAmount, totalAmount) >= totalAmount
                    ? InvoiceStatus.Paid
                    : InvoiceStatus.Pending
            };

            await _invoiceRepository.AddAsync(newInvoice, cancellationToken);
            return;
        }

        invoice.InvoiceLines.Clear();
        invoice.InvoiceLines.AddRange(returnLines);

        invoice.TotalAmount = totalAmount;
        invoice.IssueDate = issueDate;
        invoice.PaidAmount = netCompletedAmount;
        invoice.Status = Math.Min(netCompletedAmount, totalAmount) >= totalAmount
                    ? InvoiceStatus.Paid
                    : InvoiceStatus.Pending;

        await _vehicleRepository.UpdateCurrentMilage(_MileageAfter, cancellationToken);

        await _invoiceRepository.UpdateAsync(invoice, cancellationToken);
    }
}
