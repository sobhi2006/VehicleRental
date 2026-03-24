using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Payment.
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookingVehicleService _bookingVehicleService;
    private readonly ICurrencyService _currencyService;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IBookingVehicleRepository _bookingVehicleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentService"/> class.
    /// </summary>
    public PaymentService(
        IPaymentRepository repository,
        IUnitOfWork unitOfWork,
        IBookingVehicleService bookingVehicleService,
        ICurrencyService currencyService,
        IInvoiceRepository invoiceRepository,
        IBookingVehicleRepository bookingVehicleRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _bookingVehicleService = bookingVehicleService;
        _currencyService = currencyService;
        _invoiceRepository = invoiceRepository;
        _bookingVehicleRepository = bookingVehicleRepository;
    }

    /// <summary>
    /// Creates a new Payment.
    /// </summary>
    public async Task<Result<Payment>> CreateAsync(Payment request, CancellationToken cancellationToken)
    {
        var bookingExists = await _bookingVehicleService.ExistsByIdAsync(request.BookingId, cancellationToken);
        if (!bookingExists)
        {
            return Result<Payment>.Failure("Booking not found.");
        }

        var currencyExists = await _currencyService.ExistsByIdAsync(request.CurrencyId, cancellationToken);
        if (!currencyExists)
        {
            return Result<Payment>.Failure("Currency not found.");
        }

        request.Status = await DerivePaymentStatusAsync(request.BookingId, cancellationToken);

        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await SyncInvoicePaymentStateAsync(request.BookingId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing Payment.
    /// </summary>
    public async Task<Result<Payment>> UpdateAsync(Payment request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<Payment>.Failure("Payment not found.");
        }

        var bookingExists = await _bookingVehicleService.ExistsByIdAsync(request.BookingId, cancellationToken);
        if (!bookingExists)
        {
            return Result<Payment>.Failure("Booking not found.");
        }

        var currencyExists = await _currencyService.ExistsByIdAsync(request.CurrencyId, cancellationToken);
        if (!currencyExists)
        {
            return Result<Payment>.Failure("Currency not found.");
        }

        var originalBookingId = entity.BookingId;

        entity.BookingId = request.BookingId;
        entity.CurrencyId = request.CurrencyId;
        entity.Amount = request.Amount;
        entity.Type = request.Type;
        entity.Status = await DerivePaymentStatusAsync(request.BookingId, cancellationToken);

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (originalBookingId != request.BookingId)
        {
            await SyncInvoicePaymentStateAsync(originalBookingId, cancellationToken);
        }

        await SyncInvoicePaymentStateAsync(request.BookingId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing Payment.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Payment not found.");
        }

        var bookingId = entity.BookingId;

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await SyncInvoicePaymentStateAsync(bookingId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Payment by id.
    /// </summary>
    public async Task<Result<Payment>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Payment>.Failure("Payment not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all Payments with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Payment>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<Payment>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<Payment>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<Payment>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Payment>>.Success(paginated);
    }

    public Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(p => p.Id == id, cancellationToken);
    }

    private async Task SyncInvoicePaymentStateAsync(long bookingId, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetActiveByBookingIdAsync(bookingId, cancellationToken);
        if (invoice is null)
        {
            return;
        }

        var netCompletedAmount = await _repository.GetNetCompletedAmountByBookingIdAsync(bookingId, cancellationToken);

        var cappedPaidAmount = netCompletedAmount > invoice.TotalAmount
            ? invoice.TotalAmount
            : netCompletedAmount;

        invoice.PaidAmount = cappedPaidAmount;
        invoice.Status = cappedPaidAmount >= invoice.TotalAmount
            ? InvoiceStatus.Paid
            : InvoiceStatus.Pending;

        await _invoiceRepository.UpdateAsync(invoice, cancellationToken);

        await SyncBookingStatusFromInvoiceStateAsync(invoice, cancellationToken);
    }

    private async Task<PaymentStatus> DerivePaymentStatusAsync(long bookingId, CancellationToken cancellationToken)
    {
        var booking = await _bookingVehicleRepository.GetByIdAsync(bookingId, cancellationToken);
        if (booking is null)
        {
            return PaymentStatus.Pending;
        }

        return booking.Status == StatusBooking.Cancelled
            ? PaymentStatus.Cancelled
            : PaymentStatus.Completed;
    }

    private async Task SyncBookingStatusFromInvoiceStateAsync(Invoice invoice, CancellationToken cancellationToken)
    {
        var booking = await _bookingVehicleRepository.GetByIdAsync(invoice.BookingId, cancellationToken);
        if (booking is null)
        {
            return;
        }

        // Keep cancelled bookings unchanged; only sync active lifecycle states.
        if (booking.Status == StatusBooking.Cancelled)
        {
            return;
        }

        var targetStatus = invoice.Status == InvoiceStatus.Paid
            ? StatusBooking.Completed
            : StatusBooking.Confirmed;

        if (booking.Status == targetStatus)
        {
            return;
        }

        booking.Status = targetStatus;
        await _bookingVehicleRepository.UpdateAsync(booking, cancellationToken);
    }
}
