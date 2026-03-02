using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for ReturnVehicle.
/// </summary>
public class ReturnVehicleRepository : BaseRepository<ReturnVehicle>, IReturnVehicleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ReturnVehicleRepository"/> class.
    /// </summary>
    public ReturnVehicleRepository(ApplicationDbContext context) : base(context)
    {
    }

    // public override async Task<ReturnVehicle?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
