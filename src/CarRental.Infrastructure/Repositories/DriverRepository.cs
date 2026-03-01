using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Driver.
/// </summary>
public class DriverRepository : BaseRepository<Driver>, IDriverRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DriverRepository"/> class.
    /// </summary>
    public DriverRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public override async Task<Driver?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
