using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;
using CarRental.Application.Features.Payments.Commands.CreatePayment;
using CarRental.Application.Features.Payments.Commands.UpdatePayment;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Payment.
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentService"/> class.
    /// </summary>
    public PaymentService(IPaymentRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new Payment.
    /// </summary>
    public async Task<Result<PaymentDto>> CreateAsync(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Payment
        {
            BookingId = request.BookingId,
            CurrencyId = request.CurrencyId,
            Amount = request.Amount,
            Type = request.Type,
            Status = request.Status,
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
    }

    /// <summary>
    /// Updates an existing Payment.
    /// </summary>
    public async Task<Result<PaymentDto>> UpdateAsync(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<PaymentDto>.Failure("Payment not found.");
        }

        entity.BookingId = request.BookingId;
        entity.CurrencyId = request.CurrencyId;
        entity.Amount = request.Amount;
        entity.Type = request.Type;
        entity.Status = request.Status;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
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

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Payment by id.
    /// </summary>
    public async Task<Result<PaymentDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<PaymentDto>.Failure("Payment not found.");
        }

        return MapToDto(entity);
    }

    /// <summary>
    /// Gets all Payments with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<PaymentDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<PaymentDto>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<PaymentDto>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var items = entities
            .Select(MapToDto)
            .ToList();

        var paginated = new PaginatedList<PaymentDto>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<PaymentDto>>.Success(paginated);
    }

    /// <summary>
    /// Maps a domain entity to a DTO.
    /// </summary>
    private static PaymentDto MapToDto(Payment entity)
    {
        return new PaymentDto
        {
            Id = entity.Id,
            BookingId = entity.BookingId,
            CurrencyId = entity.CurrencyId,
            Amount = entity.Amount,
            Type = entity.Type,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
