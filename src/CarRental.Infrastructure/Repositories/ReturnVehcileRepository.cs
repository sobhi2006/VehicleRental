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

    public Task<ReturnVehicle?> GetByIdWithDetailsAsync(long id, CancellationToken cancellationToken)
    {
        return _dbSet
            .Include(rv => rv.ReturnVehicleFeesBanks)
            .FirstOrDefaultAsync(rv => rv.Id == id, cancellationToken);
    }

    public override Task<ReturnVehicle?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return GetByIdWithDetailsAsync(id, cancellationToken);
    }

    public override async Task<IReadOnlyList<ReturnVehicle>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(rv => rv.ReturnVehicleFeesBanks)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(rv => rv.BookingId == bookingId, cancellationToken);
    }

    public Task<bool> ExistsByBookingIdExcludeSelfAsync(long id, long bookingId, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(rv => rv.Id != id && rv.BookingId == bookingId, cancellationToken);
    }

}
