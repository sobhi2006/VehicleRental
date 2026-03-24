using CarRental.Application.Common;
using CarRental.Application.Features.DamageVehicles.Commands.CreateDamageVehicle;
using CarRental.Application.Features.DamageVehicles.Commands.UpdateDamageVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.ImageEntities;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for DamageVehicle.
/// </summary>
public class DamageVehicleService : IDamageVehicleService
{
    private readonly IDamageVehicleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DamageVehicleService"/> class.
    /// </summary>
    public DamageVehicleService(IDamageVehicleRepository repository, IUnitOfWork unitOfWork, IImageService imageService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _imageService = imageService;
    }

    /// <summary>
    /// Creates a new DamageVehicle.
    /// </summary>
    public async Task<Result<DamageVehicle>> CreateAsync(DamageVehicle request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing DamageVehicle.
    /// </summary>
    public async Task<Result<DamageVehicle>> UpdateAsync(DamageVehicle request, List<DamageVehicleImage> uploadedImages, List<long> imageIDsToRemove, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<DamageVehicle>.Failure("DamageVehicle not found.");
        }

        var imagesUrlToRemove = entity.Images
            .Where(img => imageIDsToRemove.Contains(img.Id))
            .Select(img => img.Url)
            .ToList();

        // Keep EF tracking state consistent while deleting selected images.
        imageIDsToRemove.ForEach(id => entity.Images.RemoveAll(img => img.Id == id));
        entity.Images.AddRange(uploadedImages);

        entity.VehicleId = request.VehicleId;
        entity.BookingId = request.BookingId;
        entity.Severity = request.Severity;
        entity.Description = request.Description;
        entity.RepairCost = request.RepairCost;
        entity.DamageDate = request.DamageDate;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _imageService.DeleteImagesAsync(imagesUrlToRemove, cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing DamageVehicle.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("DamageVehicle not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a DamageVehicle by id.
    /// </summary>
    public async Task<Result<DamageVehicle>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<DamageVehicle>.Failure("DamageVehicle not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all DamageVehicles with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<DamageVehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<DamageVehicle>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<DamageVehicle>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var paginated = new PaginatedList<DamageVehicle>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<DamageVehicle>>.Success(paginated);
    }

    public Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(d => d.Id == id, cancellationToken);
    }
}
