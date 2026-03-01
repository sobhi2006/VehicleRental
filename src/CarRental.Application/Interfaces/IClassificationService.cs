using CarRental.Application.Common;
using CarRental.Application.Features.Classifications.Commands.CreateClassification;
using CarRental.Application.Features.Classifications.Commands.UpdateClassification;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Classification operations.
/// </summary>
public interface IClassificationService
{
    /// <summary>
    /// Creates a new Classification.
    /// </summary>
    Task<Result<Classification>> CreateAsync(Classification request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Classification.
    /// </summary>
    Task<Result<Classification>> UpdateAsync(Classification request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Classification.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Classification by id.
    /// </summary>
    Task<Result<Classification>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Classifications with pagination.
    /// </summary>
    Task<Result<PaginatedList<Classification>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> ExistsByNameExcludeSelfAsync(UpdateClassificationCommand request, CancellationToken cancellationToken);
}
