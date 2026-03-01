using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for BookingVehicle.
/// </summary>
public interface IBookingVehicleRepository : IRepository<BookingVehicle>
{
    Task<bool> IsVehicleAvailableForBookingAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken);
}
