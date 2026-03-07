using Microsoft.AspNetCore.Http;

namespace CarRental.Application.Interfaces;

public interface IImageService
{
    Task<string> UploadImageAsync(IFormFile file, string folder, CancellationToken cancellationToken);
    Task DeleteImagesAsync(List<string> imageUrls, CancellationToken cancellationToken);
}