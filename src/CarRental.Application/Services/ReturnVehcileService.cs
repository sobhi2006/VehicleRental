using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.CreateReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.UpdateReturnVehicle;
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
    public async Task<Result<ReturnVehicleDto>> CreateAsync(CreateReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = new ReturnVehicle
        {
            BookingId = request.BookingId,
            ConditionNotes = request.ConditionNotes,
            ActualReturnDate = request.ActualReturnDate,
            MileageAfter = request.MileageAfter,
            ExcessMileageFess = request.ExcessMileageFess,
            DamageId = request.DamageId,
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
    }

    /// <summary>
    /// Updates an existing ReturnVehicle.
    /// </summary>
    public async Task<Result<ReturnVehicleDto>> UpdateAsync(UpdateReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<ReturnVehicleDto>.Failure("ReturnVehicle not found.");
        }

        entity.BookingId = request.BookingId;
        entity.ConditionNotes = request.ConditionNotes;
        entity.ActualReturnDate = request.ActualReturnDate;
        entity.MileageAfter = request.MileageAfter;
        entity.ExcessMileageFess = request.ExcessMileageFess;
        entity.DamageId = request.DamageId;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
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
    public async Task<Result<ReturnVehicleDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<ReturnVehicleDto>.Failure("ReturnVehicle not found.");
        }

        return MapToDto(entity);
    }

    /// <summary>
    /// Gets all ReturnVehicles with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<ReturnVehicleDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<ReturnVehicleDto>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<ReturnVehicleDto>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var items = entities
            .Select(MapToDto)
            .ToList();

        var paginated = new PaginatedList<ReturnVehicleDto>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<ReturnVehicleDto>>.Success(paginated);
    }

    /// <summary>
    /// Maps a domain entity to a DTO.
    /// </summary>
    private static ReturnVehicleDto MapToDto(ReturnVehicle entity)
    {
        return new ReturnVehicleDto
        {
            Id = entity.Id,
            BookingId = entity.BookingId,
            ConditionNotes = entity.ConditionNotes,
            ActualReturnDate = entity.ActualReturnDate,
            MileageAfter = entity.MileageAfter,
            ExcessMileageFess = entity.ExcessMileageFess,
            DamageId = entity.DamageId,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
