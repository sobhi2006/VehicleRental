using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Payment.
/// </summary>
public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentRepository"/> class.
    /// </summary>
    public PaymentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<decimal> GetNetCompletedAmountByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
    {
        var completedPayments = await _dbSet
            .AsNoTracking()
            .Where(p => p.BookingId == bookingId && p.Status == PaymentStatus.Completed)
            .Select(p => (p.Type == AmountType.Given ? p.Amount : -p.Amount) / p.Currency.ValueVsOneDollar)
            .SumAsync(cancellationToken);

        return completedPayments < 0 ? 0 : completedPayments;
    }

    public Task<int> CountPaymentsByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
    {
        return _dbSet.CountAsync(p => p.BookingId == bookingId, cancellationToken);
    }

    public Task<List<Payment>> GetByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(p => p.BookingId == bookingId)
            .ToListAsync(cancellationToken);
    }

    // public override async Task<Payment?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
