using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for MaintenanceVehicle.
/// </summary>
public interface IMaintenanceVehicleRepository : IRepository<MaintenanceVehicle>
{
    Task<bool> IsUnderMaintenance(long vehicleId, DateTime StartAt, DateTime EndAt, CancellationToken cancellationToken);
}
