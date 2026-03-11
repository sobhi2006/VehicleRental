using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Entities.ImageEntities;

namespace CarRental.Application.Features.Vehicles.Commands.UpdateVehicle;

/// <summary>
/// Handles updates of Vehicle.
/// </summary>
public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Result<VehicleDto>>
{
    private readonly IVehicleService _vehicleService;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateVehicleCommandHandler"/> class.
    /// </summary>
    public UpdateVehicleCommandHandler(IVehicleService service, IMapper mapper, IImageService imageService)
    {
        _vehicleService = service;
        _mapper = mapper;
        _imageService = imageService;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<VehicleDto>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Vehicle>(request);

        var uploadedImages = await Task.WhenAll(
                    request.Images.Select(file => 
                          _imageService.UploadImageAsync(file, "vehicles", cancellationToken)));

        var result = await _vehicleService.UpdateAsync(entity, 
                                                            uploadedImages.Select(url =>
                                                            new VehicleImage
                                                            {
                                                                Url = url.Path,
                                                                FakeName = url.FakeName,
                                                                FileName = url.FileName 
                                                            }).ToList(), request.ImageIDsToRemove, cancellationToken);
        return result.MapResult(value => _mapper.Map<VehicleDto>(value));
    }
}
