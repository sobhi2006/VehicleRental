using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.DamageVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.ImageEntities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.DamageVehicles.Commands.UpdateDamageVehicle;

/// <summary>
/// Handles updates of DamageVehicle.
/// </summary>
public class UpdateDamageVehicleCommandHandler : IRequestHandler<UpdateDamageVehicleCommand, Result<DamageVehicleDto>>
{
    private readonly IDamageVehicleService _service;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateDamageVehicleCommandHandler"/> class.
    /// </summary>
    public UpdateDamageVehicleCommandHandler(IDamageVehicleService service, IMapper mapper, IImageService imageService)
    {
        _service = service;
        _mapper = mapper;
        _imageService = imageService;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<DamageVehicleDto>> Handle(UpdateDamageVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DamageVehicle>(request);

        var uploadedImages = await Task.WhenAll(
            request.Images.Select(file =>
                _imageService.UploadImageAsync(file, "damageVehicles", cancellationToken)));

        var result = await _service.UpdateAsync(
            entity,
            uploadedImages.Select(url =>
                new DamageVehicleImage
                {
                    Url = url.Path,
                    FakeName = url.FakeName,
                    FileName = url.FileName
                }).ToList(),
            request.ImageIDsToRemove,
            cancellationToken);

        return result.MapResult(value => _mapper.Map<DamageVehicleDto>(value));
    }
}
