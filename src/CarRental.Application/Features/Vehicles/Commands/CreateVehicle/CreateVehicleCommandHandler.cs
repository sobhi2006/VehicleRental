using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Vehicles.Commands.CreateVehicle;

/// <summary>
/// Handles creation of Vehicle.
/// </summary>
public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommand, Result<VehicleDto>>
{
    private readonly IVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateVehicleCommandHandler"/> class.
    /// </summary>
    public CreateVehicleCommandHandler(IVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<VehicleDto>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Vehicle>(request);

        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<VehicleDto>(value));
    }
}
