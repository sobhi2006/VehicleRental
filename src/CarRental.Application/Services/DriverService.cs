using CarRental.Application.Common;
using CarRental.Application.Features.Drivers.Commands.CreateDriver;
using CarRental.Application.Features.Drivers.Commands.UpdateDriver;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Driver.
/// </summary>
public class DriverService : IDriverService
{
    private readonly IDriverRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="DriverService"/> class.
    /// </summary>
    public DriverService(IDriverRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new Driver.
    /// </summary>
    public async Task<Result<Driver>> CreateAsync(Driver request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing Driver.
    /// </summary>
    public async Task<Result<Driver>> UpdateAsync(Driver request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<Driver>.Failure("Driver not found.");
        }

        entity.PersonId = request.PersonId;
        entity.DriverLicenseNumber = request.DriverLicenseNumber;
        entity.DriverLicenseExpiryDate = request.DriverLicenseExpiryDate;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing Driver.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Driver not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Driver by id.
    /// </summary>
    public async Task<Result<Driver>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Driver>.Failure("Driver not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all Drivers with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Driver>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<Driver>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<Driver>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var paginated = new PaginatedList<Driver>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Driver>>.Success(paginated);
    }

    /// <summary>
    /// Checks if a Driver with the given PersonId already exists.
    /// </summary>
    public Task<bool> ExistsByPersonIdAsync(long personId, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(d => d.PersonId == personId, cancellationToken);
    }

    /// <summary>
    /// Checks if a Driver with the given PersonId already exists, excluding the current Driver.
    /// </summary>
    public Task<bool> ExistsByPersonIdExcludeSelfAsync(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        return _repository.ExistsExcludeSelfAsync(request.Id, d => d.PersonId == request.PersonId, cancellationToken);
    }

    /// <summary>
    /// Checks if a Driver with the given DriverLicenseNumber already exists.
    /// </summary>
    public Task<bool> ExistsByDriverLicenseNumberAsync(string driverLicenseNumber, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(
            d => string.Equals(d.DriverLicenseNumber, driverLicenseNumber, StringComparison.OrdinalIgnoreCase),
            cancellationToken);
    }

    /// <summary>
    /// Checks if a Driver with the given DriverLicenseNumber already exists, excluding the current Driver.
    /// </summary>
    public Task<bool> ExistsByDriverLicenseNumberExcludeSelfAsync(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        return _repository.ExistsExcludeSelfAsync(
            request.Id,
            d => string.Equals(d.DriverLicenseNumber, request.DriverLicenseNumber, StringComparison.OrdinalIgnoreCase),
            cancellationToken);
    }
    /// <summary>
    /// Checks if a Driver has a valid license.
    /// </summary>
    public Task<bool> IsDriverLicenseValidAsync(long driverId, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(d => d.Id == driverId && d.DriverLicenseExpiryDate > DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);
    }
}
