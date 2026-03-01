using CarRental.Application.Common;
using CarRental.Application.Features.Makes.Commands.CreateMake;
using CarRental.Application.Features.Makes.Commands.UpdateMake;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Make operations.
/// </summary>
public interface IMakeService
{
    /// <summary>
    /// Creates a new Make.
    /// </summary>
    Task<Result<Make>> CreateAsync(Make request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Make.
    /// </summary>
    Task<Result<Make>> UpdateAsync(Make request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Make.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Make by id.
    /// </summary>
    Task<Result<Make>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Makes with pagination.
    /// </summary>
    Task<Result<PaginatedList<Make>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> ExistsByNameExcludeSelfAsync(UpdateMakeCommand request, CancellationToken cancellationToken);
}
