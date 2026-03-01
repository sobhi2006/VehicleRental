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
    /// <summary>
    /// Checks if a vehicle is available for booking within the specified date range.
    /// </summary>
    public async Task<bool> IsVehicleAvailableForBookingAsync(long vehicleId, DateTime pickUpDate, DateTime dropOffDate, CancellationToken cancellationToken)
    {
        var result = await _dbSet
            .AnyAsync(bv => bv.VehicleId == vehicleId &&
                         bv.Status != StatusBooking.Cancelled 
                         && (
                                (pickUpDate >= bv.PickUpDate && pickUpDate < bv.DropOffDate) 
                                ||
                                (dropOffDate > bv.PickUpDate && dropOffDate <= bv.DropOffDate)
                            ), cancellationToken);
        return !result;
    }

    // public override async Task<BookingVehicle?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
