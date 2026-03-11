using CarRental.Application.Common;
using CarRental.Application.Features.Vehicles.Commands.CreateVehicle;
using CarRental.Application.Features.Vehicles.Commands.UpdateVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.ImageEntities;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Vehicle.
/// </summary>
public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleService"/> class.
    /// </summary>
    public VehicleService(IVehicleRepository repository, IUnitOfWork unitOfWork, IImageService imageService)
    {
        _imageService = imageService;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new Vehicle.
    /// </summary>
    public async Task<Result<Vehicle>> CreateAsync(Vehicle request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing Vehicle.
    /// </summary>
    public async Task<Result<Vehicle>> UpdateAsync(Vehicle request, List<VehicleImage> uploadedImages, List<long> ImageIDsToRemove, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<Vehicle>.Failure("Vehicle not found.");
        }

        var ImagesUrlToRemove = entity.Images.Where(img => ImageIDsToRemove.Contains(img.Id)).Select(img => img.Url).ToList();
        // Trick: To Keep Tracking in ef core.
        ImageIDsToRemove.ForEach(id => entity.Images.RemoveAll(img => img.Id == id));
        entity.Images.AddRange(uploadedImages);

        entity.MakeId = request.MakeId;
        entity.ModelYear = request.ModelYear;
        entity.VIN = request.VIN;
        entity.PlateNumber = request.PlateNumber;
        entity.CurrentMileage = request.CurrentMileage;
        entity.ClassificationId = request.ClassificationId;
        entity.Transmission = request.Transmission;
        entity.FuelType = request.FuelType;
        entity.DoorsNumber = request.DoorsNumber;
        entity.Status = request.Status;

        // await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _imageService.DeleteImagesAsync(ImagesUrlToRemove, cancellationToken);
        return entity;
    }

    /// <summary>
    /// Deletes an existing Vehicle.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Vehicle not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Vehicle by id.
    /// </summary>
    public async Task<Result<Vehicle>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Vehicle>.Failure("Vehicle not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all Vehicles with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Vehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<Vehicle>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<Vehicle>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<Vehicle>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Vehicle>>.Success(paginated);
    }

    /// <summary>
    /// Checks if a Vehicle with the given VIN already exists.
    /// </summary>
    public Task<bool> ExistsByVinAsync(string vin, CancellationToken cancellationToken)
    {
        string normalizedVin = vin.ToUpper();

        return _repository.ExistsAsync(v => v.VIN.ToUpper() == normalizedVin, cancellationToken);
    }

    /// <summary>
    /// Checks if a Vehicle with the given VIN already exists, excluding the current Vehicle.
    /// </summary>
    public Task<bool> ExistsByVinExcludeSelfAsync(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        string normalizedVin = request.VIN.ToUpper();

        return _repository.ExistsExcludeSelfAsync(
            request.Id,
            v => v.VIN.ToUpper() == normalizedVin,
            cancellationToken);
    }

    /// <summary>
    /// Checks if a Vehicle with the given PlateNumber already exists.
    /// </summary>
    public Task<bool> ExistsByPlateNumberAsync(string plateNumber, CancellationToken cancellationToken)
    {
        string normalizedPlateNumber = plateNumber.ToUpper();

        return _repository.ExistsAsync(v => v.PlateNumber.ToUpper() == normalizedPlateNumber, cancellationToken);
    }

    /// <summary>
    /// Checks if a Vehicle with the given PlateNumber already exists, excluding the current Vehicle.
    /// </summary>
    public Task<bool> ExistsByPlateNumberExcludeSelfAsync(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        string normalizedPlateNumber = request.PlateNumber.ToUpper();

        return _repository.ExistsExcludeSelfAsync(
            request.Id,
            v => v.PlateNumber.ToUpper() == normalizedPlateNumber,
            cancellationToken);
    }
    /// <summary>
    /// Checks if a vehicle is available.
    /// </summary>
    public Task<bool> IsVehicleAvailableAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken)
    {
        return _repository.IsVehicleAvailableAsync(vehicleId, pickUpDate, dropOffDate, cancellationToken);
    }
    /// <summary>
    /// Checks if a vehicle exists by its ID.
    /// </summary>
    public async Task<bool> IsExistById(long id, CancellationToken cancellationToken)
    {
        return await _repository.ExistsAsync(id, cancellationToken); 
    }
}
