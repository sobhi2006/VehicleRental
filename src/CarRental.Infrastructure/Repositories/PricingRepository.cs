using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Pricing.
/// </summary>
public class PricingRepository : BaseRepository<Pricing>, IPricingRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PricingRepository"/> class.
    /// </summary>
    public PricingRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Pricing?> GetPricingByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
    {
        return await _context.BookingVehicles.Where(b => b.Id == bookingId)
                                             .Select(b => b.Vehicle!.Classification.Pricing)
                                             .FirstOrDefaultAsync();
    }

    // public override async Task<Pricing?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
