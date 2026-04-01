using CarRental.Application.Common;
using CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;
using CarRental.Application.Features.BookingVehicles.Commands.UpdateBookingVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for BookingVehicle.
/// </summary>
public class BookingVehicleService : IBookingVehicleService
{
    private readonly IBookingVehicleRepository _repository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingVehicleService"/> class.
    /// </summary>
    public BookingVehicleService(
        IBookingVehicleRepository repository,
        IPaymentRepository paymentRepository,
        IInvoiceRepository invoiceRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _paymentRepository = paymentRepository;
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new BookingVehicle.
    /// </summary>
    public async Task<Result<BookingVehicle>> CreateAsync(BookingVehicle request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing BookingVehicle.
    /// </summary>
    public async Task<Result<BookingVehicle>> UpdateAsync(BookingVehicle request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<BookingVehicle>.Failure("BookingVehicle not found.");
        }

        entity.DriverId = request.DriverId;
        entity.VehicleId = request.VehicleId;
        entity.Status = request.Status;
        entity.Notes = request.Notes;
        entity.PickUpDate = request.PickUpDate;
        entity.DropOffDate = request.DropOffDate;

        if (entity.Status == StatusBooking.Cancelled)
        {
            await CancelRelatedEntitiesAsync(entity.Id, cancellationToken);
        }

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing BookingVehicle.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("BookingVehicle not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a BookingVehicle by id.
    /// </summary>
    public async Task<Result<BookingVehicle>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<BookingVehicle>.Failure("BookingVehicle not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all BookingVehicles with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<BookingVehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<BookingVehicle>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<BookingVehicle>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var paginated = new PaginatedList<BookingVehicle>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<BookingVehicle>>.Success(paginated);
    }
    /// <summary>
    /// Checks if a vehicle is available for booking within the specified date range.
    /// </summary>
    public Task<bool> IsVehicleAvailableForBookingAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken, long? excludeBookingVehicleId = null)
    {
        return _repository.IsVehicleAvailableForBookingAsync(vehicleId, pickUpDate, dropOffDate, cancellationToken, excludeBookingVehicleId);
    }

    public async Task<bool> IsBookingVehicleExistAsync(long id, CancellationToken ct)
    {
        return await _repository.IsBookingVehicleExistAsync(id, ct);
    }

    public Task<bool> ExistsByIdAsync(long id, CancellationToken ct)
    {
        return _repository.ExistsAsync(b => b.Id == id, ct);
    }

    private async Task CancelRelatedEntitiesAsync(long bookingId, CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetByBookingIdAsync(bookingId, cancellationToken);
        foreach (var payment in payments.Where(p => p.Status != PaymentStatus.Cancelled))
        {
            payment.Status = PaymentStatus.Cancelled;
        }

        var invoice = await _invoiceRepository.GetInvoiceByBookingIdAsync(bookingId, cancellationToken);
        if(invoice is null)
            return;
            
        invoice.Status = InvoiceStatus.Cancelled;
        invoice.PaidAmount = 0;
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<decimal> GetCurrentMilageByBookingVehicleIdAsync(long bookingVehicleId, CancellationToken cancellationToken)
    {
        return await _repository.GetCurrentMilageByBookingVehicleIdAsync(bookingVehicleId, cancellationToken);
    }
}
