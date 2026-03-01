using CarRental.Application.Common;
using CarRental.Application.Features.FeesBanks.Commands.CreateFeesBank;
using CarRental.Application.Features.FeesBanks.Commands.UpdateFeesBank;
using CarRental.Domain.Entities;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for FeesBank operations.
/// </summary>
public interface IFeesBankService
{
    /// <summary>
    /// Creates a new FeesBank.
    /// </summary>
    Task<Result<FeesBank>> CreateAsync(FeesBank request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing FeesBank.
    /// </summary>
    Task<Result<FeesBank>> UpdateAsync(FeesBank request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing FeesBank.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a FeesBank by id.
    /// </summary>
    Task<Result<FeesBank>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all FeesBanks with pagination.
    /// </summary>
    Task<Result<PaginatedList<FeesBank>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> ExistsByNameExcludeSelfAsync(UpdateFeesBankCommand request, CancellationToken cancellationToken);
}
