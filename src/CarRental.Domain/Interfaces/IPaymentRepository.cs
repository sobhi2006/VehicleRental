using CarRental.Domain.Entities;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for Payment.
/// </summary>
public interface IPaymentRepository : IRepository<Payment>
{
	Task<decimal> GetNetCompletedAmountByBookingIdAsync(long bookingId, CancellationToken cancellationToken);
	Task<int> CountPaymentsByBookingIdAsync(long bookingId, CancellationToken cancellationToken);
	Task<List<Payment>> GetByBookingIdAsync(long bookingId, CancellationToken cancellationToken);
}
