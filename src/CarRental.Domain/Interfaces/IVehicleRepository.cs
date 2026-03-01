using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for Vehicle.
/// </summary>
public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<bool> IsVehicleAvailableAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken);
}
