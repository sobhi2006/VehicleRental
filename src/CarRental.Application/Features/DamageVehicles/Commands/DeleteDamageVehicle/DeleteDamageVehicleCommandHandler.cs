using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.DamageVehicles.Commands.DeleteDamageVehicle;

/// <summary>
/// Handles deletion of DamageVehicle.
/// </summary>
public class DeleteDamageVehicleCommandHandler : IRequestHandler<DeleteDamageVehicleCommand, Result>
{
    private readonly IDamageVehicleService _service;
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDamageVehicleCommandHandler"/> class.
    /// </summary>
    public DeleteDamageVehicleCommandHandler(IDamageVehicleService service, IImageService imageService)
    {
        _service = service;
        _imageService = imageService;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteDamageVehicleCommand request, CancellationToken cancellationToken)
    {
        var damageVehicleResult = await _service.GetByIdAsync(request.Id, cancellationToken);
        if (damageVehicleResult.IsFailure || damageVehicleResult.Value is null)
        {
            return Result.Failure(damageVehicleResult.Error ?? "DamageVehicle not found.");
        }

        var imageUrlsToRemove = damageVehicleResult.Value.Images.Select(i => i.Url).ToList();
        var deleteResult = await _service.DeleteAsync(request.Id, cancellationToken);
        if (deleteResult.IsSuccess)
        {
            await _imageService.DeleteImagesAsync(imageUrlsToRemove, cancellationToken);
        }

        return deleteResult;
    }
}
