using CarRental.Application.Common;
using CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;
using CarRental.Application.Features.BookingVehicles.Commands.UpdateBookingVehicle;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for BookingVehicle operations.
/// </summary>
public interface IBookingVehicleService
{
    /// <summary>
    /// Creates a new BookingVehicle.
    /// </summary>
    Task<Result<BookingVehicle>> CreateAsync(BookingVehicle request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing BookingVehicle.
    /// </summary>
    Task<Result<BookingVehicle>> UpdateAsync(BookingVehicle request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing BookingVehicle.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a BookingVehicle by id.
    /// </summary>
    Task<Result<BookingVehicle>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all BookingVehicles with pagination.
    /// </summary>
    Task<Result<PaginatedList<BookingVehicle>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<bool> IsVehicleAvailableForBookingAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken, long? excludeBookingVehicleId = null);
    Task<bool> IsBookingVehicleExistAsync(long id, CancellationToken ct);
    Task<bool> ExistsByIdAsync(long id, CancellationToken ct);
    Task<decimal> GetCurrentMilageByBookingVehicleIdAsync(long bookingVehicleId, CancellationToken cancellationToken);
}
