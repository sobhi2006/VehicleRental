using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.Vehicles.Commands.UpdateVehicle;

/// <summary>
/// Handles updates of Vehicle.
/// </summary>
public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, Result<VehicleDto>>
{
    private readonly IVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateVehicleCommandHandler"/> class.
    /// </summary>
    public UpdateVehicleCommandHandler(IVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<VehicleDto>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Vehicle>(request);

        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<VehicleDto>(value));
    }
}
