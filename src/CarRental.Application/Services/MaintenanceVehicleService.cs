using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.UpdateMaintenanceVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for MaintenanceVehicle.
/// </summary>
public class MaintenanceVehicleService : IMaintenanceVehicleService
{
    private readonly IMaintenanceVehicleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="MaintenanceVehicleService"/> class.
    /// </summary>
    public MaintenanceVehicleService(IMaintenanceVehicleRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new MaintenanceVehicle.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> CreateAsync(CreateMaintenanceVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = new MaintenanceVehicle
        {
            VehicleId = request.VehicleId,
            Cost = request.Cost,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Notes = request.Notes,
            Status = request.Status,
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
    }

    /// <summary>
    /// Updates an existing MaintenanceVehicle.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> UpdateAsync(UpdateMaintenanceVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<MaintenanceVehicleDto>.Failure("MaintenanceVehicle not found.");
        }

        entity.VehicleId = request.VehicleId;
        entity.Cost = request.Cost;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Notes = request.Notes;
        entity.Status = request.Status;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
    }

    /// <summary>
    /// Deletes an existing MaintenanceVehicle.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("MaintenanceVehicle not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a MaintenanceVehicle by id.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<MaintenanceVehicleDto>.Failure("MaintenanceVehicle not found.");
        }

        return MapToDto(entity);
    }

    /// <summary>
    /// Gets all MaintenanceVehicles with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<MaintenanceVehicleDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<MaintenanceVehicleDto>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<MaintenanceVehicleDto>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var items = entities
            .Select(MapToDto)
            .ToList();

        var paginated = new PaginatedList<MaintenanceVehicleDto>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<MaintenanceVehicleDto>>.Success(paginated);
    }

    /// <summary>
    /// Maps a domain entity to a DTO.
    /// </summary>
    private static MaintenanceVehicleDto MapToDto(MaintenanceVehicle entity)
    {
        return new MaintenanceVehicleDto
        {
            Id = entity.Id,
            VehicleId = entity.VehicleId,
            Cost = entity.Cost,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            Notes = entity.Notes,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
