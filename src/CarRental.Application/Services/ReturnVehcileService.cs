using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for ReturnVehicle.
/// </summary>
public class ReturnVehicleService : IReturnVehicleService
{
    private readonly IReturnVehicleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReturnVehicleService"/> class.
    /// </summary>
    public ReturnVehicleService(IReturnVehicleRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new ReturnVehicle.
    /// </summary>
    public async Task<Result<ReturnVehicle>> CreateAsync(ReturnVehicle request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing ReturnVehicle.
    /// </summary>
    public async Task<Result<ReturnVehicle>> UpdateAsync(ReturnVehicle request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<ReturnVehicle>.Failure("ReturnVehicle not found.");
        }

        entity.BookingId = request.BookingId;
        entity.ConditionNotes = request.ConditionNotes;
        entity.ActualReturnDate = request.ActualReturnDate;
        entity.MileageAfter = request.MileageAfter;
        entity.ExcessMileageFess = request.ExcessMileageFess;
        entity.DamageId = request.DamageId;

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
}
