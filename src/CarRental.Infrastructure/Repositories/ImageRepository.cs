using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly ApplicationDbContext _context;

    public ImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> GetImageUrlsByIdsAsync<T>(List<long> imageIDsToRemove, CancellationToken cancellationToken) where T : Image
    {
        var urls = await _context.Set<T>()
            .AsNoTracking()
            .Where(img => imageIDsToRemove.Contains(img.Id))
            .Select(img => img.Url)
            .ToListAsync();

        return urls;
    }

    public async Task<List<string>> GetImageUrlsByVehicleIdAsync(long vehicleId, CancellationToken cancellationToken)
    {
        var urls = await _context.VehicleImages
            .AsNoTracking()
            .Where(img => img.VehicleId == vehicleId)
            .Select(img => img.Url)
            .ToListAsync(cancellationToken);

        return urls;
    }
}