using CarRental.Application.Common;
using CarRental.Application.Features.DamageVehicles.Commands.CreateDamageVehicle;
using CarRental.Application.Features.DamageVehicles.Commands.UpdateDamageVehicle;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for DamageVehicle operations.
/// </summary>
public interface IDamageVehicleService
{
    /// <summary>
    /// Creates a new DamageVehicle.
    /// </summary>
    Task<Result<DamageVehicle>> CreateAsync(DamageVehicle request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing DamageVehicle.
    /// </summary>
    Task<Result<DamageVehicle>> UpdateAsync(DamageVehicle request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing DamageVehicle.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a DamageVehicle by id.
    /// </summary>
    Task<Result<DamageVehicle>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all DamageVehicles with pagination.
    /// </summary>
    Task<Result<PaginatedList<DamageVehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
