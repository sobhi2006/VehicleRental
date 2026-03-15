using CarRental.Application.Common;
using CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for MaintenanceVehicle operations.
/// </summary>
public interface IMaintenanceVehicleService
{
    /// <summary>
    /// Creates a new MaintenanceVehicle.
    /// </summary>
    Task<Result<MaintenanceVehicle>> CreateAsync(MaintenanceVehicle request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing MaintenanceVehicle.
    /// </summary>
    Task<Result<MaintenanceVehicle>> UpdateAsync(MaintenanceVehicle request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing MaintenanceVehicle.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a MaintenanceVehicle by id.
    /// </summary>
    Task<Result<MaintenanceVehicle>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all MaintenanceVehicles with pagination.
    /// </summary>
    Task<Result<PaginatedList<MaintenanceVehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> IsUnderMaintenanceAsync(long vehicleId, DateTime StartAt, DateTime EndAt, CancellationToken cancellationToken);
}
