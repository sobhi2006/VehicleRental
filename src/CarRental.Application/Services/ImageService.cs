using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CarRental.Application.Services;

public class ImageService : IImageService
{
    public Task DeleteImagesAsync(List<string> imageUrls, CancellationToken cancellationToken)
    {
        foreach (var url in imageUrls)
        {
            var filePath = Path.Combine("wwwroot", url.Replace("/Upload/", "Upload\\"));
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        return Task.CompletedTask;
    }

    public async Task<string> UploadImageAsync(IFormFile file, string folder, CancellationToken cancellationToken)
    {
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine("wwwroot", "Upload", folder, fileName);

        if(!Directory.Exists(Path.GetDirectoryName(filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        }
        
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return filePath;
    }
}