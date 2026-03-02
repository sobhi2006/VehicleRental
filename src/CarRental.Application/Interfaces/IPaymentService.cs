using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;
using CarRental.Application.Features.Payments.Commands.CreatePayment;
using CarRental.Application.Features.Payments.Commands.UpdatePayment;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Payment operations.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Creates a new Payment.
    /// </summary>
    Task<Result<PaymentDto>> CreateAsync(CreatePaymentCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Payment.
    /// </summary>
    Task<Result<PaymentDto>> UpdateAsync(UpdatePaymentCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Payment.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Payment by id.
    /// </summary>
    Task<Result<PaymentDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Payments with pagination.
    /// </summary>
    Task<Result<PaginatedList<PaymentDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
