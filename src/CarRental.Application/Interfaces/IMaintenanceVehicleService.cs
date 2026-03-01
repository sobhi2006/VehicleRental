using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.UpdateMaintenanceVehicle;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for MaintenanceVehicle operations.
/// </summary>
public interface IMaintenanceVehicleService
{
    /// <summary>
    /// Creates a new MaintenanceVehicle.
    /// </summary>
    Task<Result<MaintenanceVehicleDto>> CreateAsync(CreateMaintenanceVehicleCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing MaintenanceVehicle.
    /// </summary>
    Task<Result<MaintenanceVehicleDto>> UpdateAsync(UpdateMaintenanceVehicleCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing MaintenanceVehicle.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a MaintenanceVehicle by id.
    /// </summary>
    Task<Result<MaintenanceVehicleDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all MaintenanceVehicles with pagination.
    /// </summary>
    Task<Result<PaginatedList<MaintenanceVehicleDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
