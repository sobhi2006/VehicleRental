using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for BookingVehicle.
/// </summary>
public interface IBookingVehicleRepository : IRepository<BookingVehicle>
{
    Task<decimal> GetCurrentMilageByBookingVehicleIdAsync(long bookingVehicleId, CancellationToken cancellationToken);
    Task<bool> IsBookingVehicleExistAsync(long id, CancellationToken ct);
    Task<bool> IsVehicleAvailableForBookingAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken, long? excludeBookingVehicleId = null);
}
