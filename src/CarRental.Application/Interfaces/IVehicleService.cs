using CarRental.Application.Common;
using CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;
using CarRental.Application.Features.Vehicles.Commands.CreateVehicle;
using CarRental.Application.Features.Vehicles.Commands.UpdateVehicle;
using CarRental.Domain.Entities.ImageEntities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Vehicle operations.
/// </summary>
public interface IVehicleService
{
    /// <summary>
    /// Creates a new Vehicle.
    /// </summary>
    Task<Result<Vehicle>> CreateAsync(Vehicle request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Vehicle.
    /// </summary>
    Task<Result<Vehicle>> UpdateAsync(Vehicle request, List<VehicleImage> uploadedImages, List<long> ImageIDsToRemove, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Vehicle.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Vehicle by id.
    /// </summary>
    Task<Result<Vehicle>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Vehicles with pagination.
    /// </summary>
    Task<Result<PaginatedList<Vehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> ExistsByVinAsync(string vin, CancellationToken cancellationToken);
    Task<bool> ExistsByVinExcludeSelfAsync(UpdateVehicleCommand request, CancellationToken cancellationToken);
    Task<bool> ExistsByPlateNumberAsync(string plateNumber, CancellationToken cancellationToken);
    Task<bool> ExistsByPlateNumberExcludeSelfAsync(UpdateVehicleCommand request, CancellationToken cancellationToken);
    Task<bool> IsVehicleAvailableAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken);
    Task<bool> IsExistById(long id, CancellationToken cancellationToken);
}
