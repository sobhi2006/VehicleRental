using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for FeesBank.
/// </summary>
public class FeesBankRepository : BaseRepository<FeesBank>, IFeesBankRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FeesBankRepository"/> class.
    /// </summary>
    public FeesBankRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public override async Task<FeesBank?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
