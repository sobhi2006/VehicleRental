using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for Vehicle.
/// </summary>
public interface IVehicleRepository : IRepository<Vehicle>
{
    Task<bool> IsVehicleAvailableAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken);
    Task UpdateStatus(long vehicleId, StatusVehicle maintenance, CancellationToken cancellationToken);
}
