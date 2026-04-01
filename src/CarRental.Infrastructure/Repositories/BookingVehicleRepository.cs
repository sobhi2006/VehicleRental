using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Enums;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for BookingVehicle.
/// </summary>
public class BookingVehicleRepository : BaseRepository<BookingVehicle>, IBookingVehicleRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookingVehicleRepository"/> class.
    /// </summary>
    public BookingVehicleRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<decimal> GetCurrentMilageByBookingVehicleIdAsync(long bookingVehicleId, CancellationToken cancellationToken)
    {
        return await _dbSet.Where(b => b.Id == bookingVehicleId)
                           .Select(b => b.Vehicle!.CurrentMileage)
                           .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsBookingVehicleExistAsync(long id, CancellationToken ct)
    {
        return await _dbSet.AnyAsync(bv => bv.Id == id 
                                                     && (
                                                            bv.Status != StatusBooking.Completed 
                                                            && 
                                                            bv.Status != StatusBooking.Cancelled
                                                        ), ct);
    }

    /// <summary>
    /// Checks if a vehicle is available for booking within the specified date range.
    /// </summary>
    public async Task<bool> IsVehicleAvailableForBookingAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken, long? excludeBookingVehicleId = null)
    {
        var result = await _dbSet
            .AnyAsync(bv =>bv.VehicleId == vehicleId &&
                                                    (!excludeBookingVehicleId.HasValue || bv.Id != excludeBookingVehicleId.Value) &&
                                                    bv.PickUpDate < dropOffDate &&
                                                    bv.DropOffDate > pickUpDate &&
                                                    bv.Status != StatusBooking.Cancelled &&  
                                                    bv.Status != StatusBooking.Completed
                                                    , cancellationToken);
        return !result;
    }

    // public override async Task<BookingVehicle?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
