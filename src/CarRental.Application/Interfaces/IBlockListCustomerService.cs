using CarRental.Application.Common;
using CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;
using CarRental.Domain.Entities;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for BlockListCustomer operations.
/// </summary>
public interface IBlockListCustomerService
{
    /// <summary>
    /// Creates a new BlockListCustomer.
    /// </summary>
    Task<Result<BlockListCustomer>> CreateAsync(BlockListCustomer entity, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing BlockListCustomer.
    /// </summary>
    Task<Result<BlockListCustomer>> UpdateAsync(BlockListCustomer entity, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing BlockListCustomer.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a BlockListCustomer by id.
    /// </summary>
    Task<Result<BlockListCustomer>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all BlockListCustomers with pagination.
    /// </summary>
    Task<Result<PaginatedList<BlockListCustomer>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> IsDriverBlockedById(long driverId, CancellationToken ct);
}
