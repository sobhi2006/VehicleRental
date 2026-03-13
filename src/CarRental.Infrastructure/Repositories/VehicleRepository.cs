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
        // Vehicle must exist and be available
        var isVehicleAvailable = await _dbSet.AnyAsync(
            v => v.Id == vehicleId && v.Status == StatusVehicle.Available,
            cancellationToken);

        if (!isVehicleAvailable)
        {
            return false;
        }

        // Standard overlap rule: existingStart < requestedEnd && existingEnd > requestedStart
        var hasMaintenanceConflict = await _context.Set<MaintenanceVehicle>().AnyAsync(
            mv =>
                mv.VehicleId == vehicleId &&
                (mv.Status == MaintenanceStatus.InProgress || mv.Status == MaintenanceStatus.Scheduled) &&
                mv.StartDate < dropOffDate &&
                mv.EndDate > pickUpDate,
            cancellationToken);

        if (hasMaintenanceConflict)
        {
            return false;
        }

        var hasBookingConflict = await _context.Set<BookingVehicle>().AnyAsync(
            bv =>
                bv.VehicleId == vehicleId &&
                bv.PickUpDate < dropOffDate &&
                bv.DropOffDate > pickUpDate &&
                (bv.Status == StatusBooking.Cancelled || bv.Status == StatusBooking.Completed),
            cancellationToken);

        return !hasBookingConflict;
    }

    public override async Task<Vehicle?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Images)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public override async Task<IReadOnlyList<Vehicle>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.Images)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public Task UpdateStatus(long vehicleId, StatusVehicle maintenance, CancellationToken cancellationToken)
    {
        return _dbSet.Where(v => v.Id == vehicleId)
            .ExecuteUpdateAsync(s => s.SetProperty(v => v.Status, maintenance), cancellationToken);
    }
}
