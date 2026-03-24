using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Invoice.
/// </summary>
public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBookingVehicleService _bookingVehicleService;
    private readonly IPaymentRepository _paymentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceService"/> class.
    /// </summary>
    public InvoiceService(
        IInvoiceRepository repository,
        IUnitOfWork unitOfWork,
        IBookingVehicleService bookingVehicleService,
        IPaymentRepository paymentRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _bookingVehicleService = bookingVehicleService;
        _paymentRepository = paymentRepository;
    }

    /// <summary>
    /// Creates a new Invoice.
    /// </summary>
    public async Task<Result<Invoice>> CreateAsync(Invoice entity, CancellationToken cancellationToken)
    {
        var bookingResult = await _bookingVehicleService.GetByIdAsync(entity.BookingId, cancellationToken);
        if (bookingResult.IsFailure)
        {
            return Result<Invoice>.Failure("Booking not found.");
        }

        if (bookingResult.Value?.Status == StatusBooking.Cancelled)
        {
            return Result<Invoice>.Failure("Cannot issue invoice for a cancelled booking.");
        }

        var hasActiveInvoice = await _repository.ExistsActiveByBookingIdAsync(entity.BookingId, cancellationToken);
        if (hasActiveInvoice)
        {
            return Result<Invoice>.Failure("Only one active invoice is allowed per booking.");
        }

        NormalizeInvoiceAmounts(entity);

        var amountValidation = await ValidateInvoiceIssueRulesAsync(entity, cancellationToken);
        if (amountValidation is not null)
        {
            return Result<Invoice>.Failure(amountValidation);
        }

        entity.PaidAmount = entity.TotalAmount;
        entity.Status = InvoiceStatus.Paid;

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Invoice>.Success(entity);
    }

    /// <summary>
    /// Updates an existing Invoice.
    /// </summary>
    public async Task<Result<Invoice>> UpdateAsync(Invoice entity, CancellationToken cancellationToken)
    {
        var existingEntity = await _repository.GetByIdAsync(entity.Id, cancellationToken);
        if (existingEntity is null)
        {
            return Result<Invoice>.Failure("Invoice not found.");
        }

        var bookingResult = await _bookingVehicleService.GetByIdAsync(entity.BookingId, cancellationToken);
        if (bookingResult.IsFailure)
        {
            return Result<Invoice>.Failure("Booking not found.");
        }

        if (bookingResult.Value?.Status == StatusBooking.Cancelled)
        {
            return Result<Invoice>.Failure("Cannot issue invoice for a cancelled booking.");
        }

        var hasActiveInvoice = await _repository.ExistsActiveByBookingIdExcludeSelfAsync(entity.Id, entity.BookingId, cancellationToken);
        if (hasActiveInvoice)
        {
            return Result<Invoice>.Failure("Only one active invoice is allowed per booking.");
        }

        existingEntity.BookingId = entity.BookingId;
        existingEntity.IssueDate = entity.IssueDate;

        existingEntity.InvoiceLines.Clear();
        foreach (var line in entity.InvoiceLines)
        {
            existingEntity.InvoiceLines.Add(new InvoiceLine
            {
                Description = line.Description,
                Quantity = line.Quantity,
                UnitPrice = line.UnitPrice,
                LineTotal = line.Quantity * line.UnitPrice
            });
        }

        NormalizeInvoiceAmounts(existingEntity);

        var amountValidation = await ValidateInvoiceIssueRulesAsync(existingEntity, cancellationToken);
        if (amountValidation is not null)
        {
            return Result<Invoice>.Failure(amountValidation);
        }

        existingEntity.PaidAmount = existingEntity.TotalAmount;
        existingEntity.Status = InvoiceStatus.Paid;

        await _repository.UpdateAsync(existingEntity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Invoice>.Success(existingEntity);
    }

    /// <summary>
    /// Deletes an existing Invoice.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Invoice not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Invoice by id.
    /// </summary>
    public async Task<Result<Invoice>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Invoice>.Failure("Invoice not found.");
        }

        return Result<Invoice>.Success(entity);
    }

    /// <summary>
    /// Gets all Invoices with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Invoice>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<Invoice>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<Invoice>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var items = entities.ToList();
        var paginated = new PaginatedList<Invoice>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Invoice>>.Success(paginated);
    }

    public Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(i => i.Id == id, cancellationToken);
    }

    private static void NormalizeInvoiceAmounts(Invoice entity)
    {
        if (entity.InvoiceLines.Count == 0)
        {
            return;
        }

        foreach (var line in entity.InvoiceLines)
        {
            line.LineTotal = line.Quantity * line.UnitPrice;
        }

        entity.TotalAmount = entity.InvoiceLines.Sum(line => line.LineTotal);
    }

    private async Task<string?> ValidateInvoiceIssueRulesAsync(Invoice entity, CancellationToken cancellationToken)
    {
        var completedAmount = await _paymentRepository.GetNetCompletedAmountByBookingIdAsync(entity.BookingId, cancellationToken);
        if (completedAmount < entity.TotalAmount)
        {
            return "Invoice can only be issued after full payment is completed.";
        }

        if (entity.InvoiceLines.Count == 0)
        {
            return "Invoice must contain at least one invoice line.";
        }

        return null;
    }
}
