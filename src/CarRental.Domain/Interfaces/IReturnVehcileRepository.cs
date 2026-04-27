using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for ReturnVehicle.
/// </summary>
public interface IReturnVehicleRepository : IRepository<ReturnVehicle>
{
	Task<ReturnVehicle?> GetByIdWithDetailsAsync(long id, CancellationToken cancellationToken);
	Task<bool> ExistsByBookingIdAsync(long bookingId, CancellationToken cancellationToken);
	Task<bool> ExistsByBookingIdExcludeSelfAsync(long id, long bookingId, CancellationToken cancellationToken);
}
