using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<FeesBank>> GetByIdsAsync(IReadOnlyCollection<long> ids, CancellationToken cancellationToken)
    {
        if (ids.Count == 0)
        {
            return await Task.FromResult(new List<FeesBank>());
        }

        return await _dbSet.Where(f => ids.Contains(f.Id)).ToListAsync(cancellationToken);
    }

    public async Task<int> CountByIdsAsync(IReadOnlyCollection<long> ids, CancellationToken cancellationToken)
    {
        if (ids.Count == 0)
        {
            return await Task.FromResult(0);
        }

        return await _dbSet.CountAsync(f => ids.Contains(f.Id), cancellationToken);
    }

    // public override async Task<FeesBank?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
