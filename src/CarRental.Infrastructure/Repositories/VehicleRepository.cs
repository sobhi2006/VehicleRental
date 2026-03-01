using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Vehicle.
/// </summary>
public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VehicleRepository"/> class.
    /// </summary>
    public VehicleRepository(ApplicationDbContext context) : base(context)
    {
    }
    /// <summary>
    /// Checks if a vehicle is available.
    /// </summary>
    public async Task<bool> IsVehicleAvailableAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(v => v.Id == vehicleId 
            && v.Status == StatusVehicle.Available, cancellationToken);
    }

    // public override async Task<Vehicle?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
