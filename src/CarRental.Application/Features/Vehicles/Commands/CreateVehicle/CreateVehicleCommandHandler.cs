using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Entities.ImageEntities;

namespace CarRental.Application.Features.Vehicles.Commands.CreateVehicle;

/// <summary>
/// Handles creation of Vehicle.
/// </summary>
public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<VehicleDto>>
{
    private readonly IVehicleService _service;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
    /// </summary>
    public CreateVehicleCommandHandler(IVehicleService service, IMapper mapper, IImageService imageService)
    {
        _service = service;
        _mapper = mapper;
        _imageService = imageService;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<VehicleDto>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Vehicle>(request);
        if (request.Images != null && request.Images.Count > 0)
        {
            
            var uploadedImages = await Task.WhenAll(
                    request.Images.Select(file => 
                          _imageService.UploadImageAsync(file, "vehicles", cancellationToken)));

            entity.Images = uploadedImages.Select(url => 
                new VehicleImage 
                {
                    Url = url.Path,
                    FakeName = url.FakeName,
                    FileName = url.FileName 
                }).ToList();
        }

        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<VehicleDto>(value));
    }
}
