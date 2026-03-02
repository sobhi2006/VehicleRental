using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.CreateReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.UpdateReturnVehicle;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for ReturnVehicle operations.
/// </summary>
public interface IReturnVehicleService
{
    /// <summary>
    /// Creates a new ReturnVehicle.
    /// </summary>
    Task<Result<ReturnVehicleDto>> CreateAsync(CreateReturnVehicleCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing ReturnVehicle.
    /// </summary>
    Task<Result<ReturnVehicleDto>> UpdateAsync(UpdateReturnVehicleCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing ReturnVehicle.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a ReturnVehicle by id.
    /// </summary>
    Task<Result<ReturnVehicleDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all ReturnVehicles with pagination.
    /// </summary>
    Task<Result<PaginatedList<ReturnVehicleDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
