using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.UpdateMaintenanceVehicle;

/// <summary>
/// Handles updates of MaintenanceVehicle.
/// </summary>
public class UpdateMaintenanceVehicleCommandHandler : IRequestHandler<UpdateMaintenanceVehicleCommand, Result<MaintenanceVehicleDto>>
{
    private readonly IMaintenanceVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateMaintenanceVehicleCommandHandler"/> class.
    /// </summary>
    public UpdateMaintenanceVehicleCommandHandler(IMaintenanceVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> Handle(UpdateMaintenanceVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(request, cancellationToken);
    }
}
