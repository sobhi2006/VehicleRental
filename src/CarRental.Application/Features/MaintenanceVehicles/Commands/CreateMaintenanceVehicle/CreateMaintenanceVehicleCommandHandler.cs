using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Interfaces;
using AutoMapper;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;

/// <summary>
/// Handles creation of MaintenanceVehicle.
/// </summary>
public class CreateMaintenanceVehicleCommandHandler : IRequestHandler<CreateMaintenanceVehicleCommand, Result<MaintenanceVehicleDto>>
{
    private readonly IMaintenanceVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMaintenanceVehicleCommandHandler"/> class.
    /// </summary>
    public CreateMaintenanceVehicleCommandHandler(IMaintenanceVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> Handle(CreateMaintenanceVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<MaintenanceVehicle>(request);
        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<MaintenanceVehicleDto>(value));
    }
}
