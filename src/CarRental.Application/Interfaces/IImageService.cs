using Microsoft.AspNetCore.Http;

namespace CarRental.Application.Interfaces;

public interface IImageService
{
    Task<(string Path, string FileName, string FakeName)> UploadImageAsync(IFormFile file, string folder, CancellationToken cancellationToken);
    Task DeleteImagesAsync(List<string> imageUrls, CancellationToken cancellationToken);
    Task<List<string>> GetImageUrlsVehiclesByIds(List<long> imageIDsToRemove, CancellationToken cancellationToken);
    Task<List<string>> GetImageUrlsDamageVehiclesByIds(List<long> imageIDsToRemove, CancellationToken cancellationToken);
    Task<List<string>> GetImageUrlsByVehicleId(long vehicleId, CancellationToken cancellationToken);
}