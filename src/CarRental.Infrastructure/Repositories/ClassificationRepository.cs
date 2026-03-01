using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Classification.
/// </summary>
public class ClassificationRepository : BaseRepository<Classification>, IClassificationRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClassificationRepository"/> class.
    /// </summary>
    public ClassificationRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public override async Task<Classification?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
