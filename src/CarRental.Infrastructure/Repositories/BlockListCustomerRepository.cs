using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for BlockListCustomer.
/// </summary>
public class BlockListCustomerRepository : BaseRepository<BlockListCustomer>, IBlockListCustomerRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockListCustomerRepository"/> class.
    /// </summary>
    public BlockListCustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public override async Task<BlockListCustomer?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
