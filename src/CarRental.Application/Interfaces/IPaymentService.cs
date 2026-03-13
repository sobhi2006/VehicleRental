using CarRental.Application.Common;
using CarRental.Domain.Entities;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Payment operations.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Creates a new Payment.
    /// </summary>
    Task<Result<Payment>> CreateAsync(Payment request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Payment.
    /// </summary>
    Task<Result<Payment>> UpdateAsync(Payment request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Payment.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Payment by id.
    /// </summary>
    Task<Result<Payment>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Payments with pagination.
    /// </summary>
    Task<Result<PaginatedList<Payment>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
