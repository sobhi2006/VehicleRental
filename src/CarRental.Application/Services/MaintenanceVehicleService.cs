using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for MaintenanceVehicle.
/// </summary>
public class MaintenanceVehicleService : IMaintenanceVehicleService
{
    private readonly IMaintenanceVehicleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IVehicleRepository _vehicleRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="MaintenanceVehicleService"/> class.
    /// </summary>
    public MaintenanceVehicleService(IMaintenanceVehicleRepository repository, IUnitOfWork unitOfWork, IVehicleRepository vehicleRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _vehicleRepository = vehicleRepository;
    }

    /// <summary>
    /// Creates a new MaintenanceVehicle.
    /// </summary>
    public async Task<Result<MaintenanceVehicle>> CreateAsync(MaintenanceVehicle request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if(request.Status == MaintenanceStatus.Scheduled)
        {
            await _vehicleRepository.UpdateStatus(request.VehicleId, StatusVehicle.Maintenance, cancellationToken);
        }

        return request;
    }

    /// <summary>
    /// Updates an existing MaintenanceVehicle.
    /// </summary>
        public async Task<Result<MaintenanceVehicle>> UpdateAsync(MaintenanceVehicle request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<MaintenanceVehicle>.Failure("MaintenanceVehicle not found.");
        }

        entity.VehicleId = request.VehicleId;
        entity.Cost = request.Cost;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.Notes = request.Notes;
        entity.Status = request.Status;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if(request.Status == MaintenanceStatus.Scheduled)
        {
            await _vehicleRepository.UpdateStatus(request.VehicleId, StatusVehicle.Maintenance, cancellationToken);
        }

        return entity;
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
    public async Task<Result<MaintenanceVehicle>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
                return Result<MaintenanceVehicle>.Failure("MaintenanceVehicle not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all MaintenanceVehicles with pagination.
    /// </summary>
        public async Task<Result<PaginatedList<MaintenanceVehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
                return Result<PaginatedList<MaintenanceVehicle>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
                return Result<PaginatedList<MaintenanceVehicle>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<MaintenanceVehicle>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<MaintenanceVehicle>>.Success(paginated);
    }

    public async Task<bool> IsUnderMaintenanceAsync(long vehicleId, DateTime StartAt, DateTime EndAt, CancellationToken cancellationToken)
    {
        return await _repository.IsUnderMaintenance(vehicleId, StartAt, EndAt, cancellationToken);
    }
}
