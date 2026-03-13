using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.UpdateMaintenanceVehicle;

/// <summary>
/// Handles updates of MaintenanceVehicle.
/// </summary>
public class UpdateMaintenanceVehicleCommandHandler : IRequestHandler<UpdateMaintenanceVehicleCommand, Result<MaintenanceVehicleDto>>
{
    private readonly IMaintenanceVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMaintenanceVehicleCommandHandler"/> class.
    /// </summary>
    public UpdateMaintenanceVehicleCommandHandler(IMaintenanceVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> Handle(UpdateMaintenanceVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<MaintenanceVehicle>(request);
        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<MaintenanceVehicleDto>(value));
    }
}
