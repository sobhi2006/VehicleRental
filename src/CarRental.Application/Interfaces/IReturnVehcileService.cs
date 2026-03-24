using CarRental.Application.Common;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for ReturnVehicle operations.
/// </summary>
public interface IReturnVehicleService
{
    /// <summary>
    /// Creates a new ReturnVehicle.
    /// </summary>
    Task<Result<ReturnVehicle>> CreateAsync(ReturnVehicle request, IReadOnlyCollection<long> feesBankIds, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing ReturnVehicle.
    /// </summary>
    Task<Result<ReturnVehicle>> UpdateAsync(ReturnVehicle request, IReadOnlyCollection<long> feesBankIds, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing ReturnVehicle.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a ReturnVehicle by id.
    /// </summary>
    Task<Result<ReturnVehicle>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all ReturnVehicles with pagination.
    /// </summary>
    Task<Result<PaginatedList<ReturnVehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken);
    Task<bool> ExistsByBookingIdAsync(long bookingId, CancellationToken cancellationToken);
    Task<bool> ExistsByBookingIdExcludeSelfAsync(long id, long bookingId, CancellationToken cancellationToken);
}
