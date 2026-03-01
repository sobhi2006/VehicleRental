using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Person.
/// </summary>
public class PersonRepository : BaseRepository<Person>, IPersonRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PersonRepository"/> class.
    /// </summary>
    public PersonRepository(ApplicationDbContext context) : base(context)
    {
    }
    // public override async Task<Person?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
