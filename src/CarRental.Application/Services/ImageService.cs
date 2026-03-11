using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.ImageEntities;
using CarRental.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CarRental.Application.Services;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;

    public ImageService(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }
    public Task DeleteImagesAsync(List<string> imageUrls, CancellationToken cancellationToken)
    {
        foreach (var url in imageUrls)
        {
            var filePath = Path.Combine("wwwroot", url);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        return Task.CompletedTask;
    }

    public async Task<List<string>> GetImageUrlsByVehicleId(long vehicleId, CancellationToken cancellationToken)
    {
        return await _imageRepository.GetImageUrlsByVehicleIdAsync(vehicleId, cancellationToken);
    }

    public async Task<List<string>> GetImageUrlsDamageVehiclesByIds(List<long> imageIDsToRemove, CancellationToken cancellationToken)
    {
        return await _imageRepository.GetImageUrlsByIdsAsync<DamageVehicleImage>(imageIDsToRemove, cancellationToken);
    }

    public async Task<List<string>> GetImageUrlsVehiclesByIds(List<long> imageIDsToRemove, CancellationToken cancellationToken)
    {
        return await _imageRepository.GetImageUrlsByIdsAsync<VehicleImage>(imageIDsToRemove, cancellationToken);
    }

    public async Task<(string Path, string FileName, string FakeName)> UploadImageAsync(IFormFile file, string folder, CancellationToken cancellationToken)
    {
        var FakeName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine("wwwroot", "Upload", folder, FakeName);

        if(!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        }
        
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return (filePath.Replace("wwwroot/", ""), file.FileName, FakeName);
    }
}