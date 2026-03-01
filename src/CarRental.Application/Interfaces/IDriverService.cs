using CarRental.Application.Common;
using CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;
using CarRental.Application.Features.Drivers.Commands.CreateDriver;
using CarRental.Application.Features.Drivers.Commands.UpdateDriver;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Driver operations.
/// </summary>
public interface IDriverService
{
    /// <summary>
    /// Creates a new Driver.
    /// </summary>
    Task<Result<Driver>> CreateAsync(Driver request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Driver.
    /// </summary>
    Task<Result<Driver>> UpdateAsync(Driver request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Driver.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Driver by id.
    /// </summary>
    Task<Result<Driver>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Drivers with pagination.
    /// </summary>
    Task<Result<PaginatedList<Driver>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> ExistsByPersonIdAsync(long personId, CancellationToken cancellationToken);
    Task<bool> ExistsByPersonIdExcludeSelfAsync(UpdateDriverCommand request, CancellationToken cancellationToken);
    Task<bool> ExistsByDriverLicenseNumberAsync(string driverLicenseNumber, CancellationToken cancellationToken);
    Task<bool> ExistsByDriverLicenseNumberExcludeSelfAsync(UpdateDriverCommand request, CancellationToken cancellationToken);
    Task<bool> IsDriverLicenseValidAsync(long driverId, CancellationToken cancellationToken);
}
