using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for MaintenanceVehicle.
/// </summary>
public class MaintenanceVehicleRepository : BaseRepository<MaintenanceVehicle>, IMaintenanceVehicleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MaintenanceVehicleRepository"/> class.
    /// </summary>
    public MaintenanceVehicleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<bool> IsUnderMaintenance(long vehicleId, DateTime StartAt, DateTime EndAt, CancellationToken cancellationToken)
    {
        return await _dbSet.AnyAsync(mv => mv.VehicleId == vehicleId && (mv.Status == MaintenanceStatus.InProgress || mv.Status == MaintenanceStatus.Scheduled) &&
                mv.StartDate < EndAt &&
                mv.EndDate > StartAt, cancellationToken);
    }

    // public override async Task<MaintenanceVehicle?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
