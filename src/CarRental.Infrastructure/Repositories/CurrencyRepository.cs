using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Currency.
/// </summary>
public class CurrencyRepository : BaseRepository<Currency>, ICurrencyRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyRepository"/> class.
    /// </summary>
    public CurrencyRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public override async Task<Currency?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
