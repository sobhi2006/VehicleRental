using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Vehicles.Commands.DeleteVehicle;

/// <summary>
/// Handles deletion of Vehicle.
/// </summary>
public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Result>
{
    private readonly IVehicleService _vehicleService;
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteVehicleCommandHandler"/> class.
    /// </summary>
    public DeleteVehicleCommandHandler(IVehicleService vehicleService, IImageService imageService)
    {
        _vehicleService = vehicleService;
        _imageService = imageService;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        var imagesUrlToRemove = await _imageService.GetImageUrlsByVehicleId(request.Id, cancellationToken);
        var result = await _vehicleService.DeleteAsync(request.Id, cancellationToken);
        if (result.IsSuccess)
        {
            await _imageService.DeleteImagesAsync(imagesUrlToRemove, cancellationToken);
        }
        return result;
    }
}
