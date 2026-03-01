using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Make.
/// </summary>
public class MakeRepository : BaseRepository<Make>, IMakeRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MakeRepository"/> class.
    /// </summary>
    public MakeRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public override async Task<Make?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
