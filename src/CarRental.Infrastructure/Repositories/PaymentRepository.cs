using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
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

    // public override async Task<Payment?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
