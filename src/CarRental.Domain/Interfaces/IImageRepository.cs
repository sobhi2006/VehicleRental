using CarRental.Domain.Entities;

namespace CarRental.Domain.Interfaces;

public interface IImageRepository
{
    /// <summary>
    /// Gets the image URLs by their IDs.
    /// </summary>
    Task<List<string>> GetImageUrlsByIdsAsync<T>(List<long> imageIDsToRemove, CancellationToken cancellationToken) where T : Image;
    Task<List<string>> GetImageUrlsByVehicleIdAsync(long vehicleId, CancellationToken cancellationToken);
}