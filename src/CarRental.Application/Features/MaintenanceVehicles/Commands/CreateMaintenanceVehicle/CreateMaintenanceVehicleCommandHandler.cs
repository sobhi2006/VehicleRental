using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;

/// <summary>
/// Handles creation of MaintenanceVehicle.
/// </summary>
public class CreateMaintenanceVehicleCommandHandler : IRequestHandler<CreateMaintenanceVehicleCommand, Result<MaintenanceVehicleDto>>
{
    private readonly IMaintenanceVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateMaintenanceVehicleCommandHandler"/> class.
    /// </summary>
    public CreateMaintenanceVehicleCommandHandler(IMaintenanceVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> Handle(CreateMaintenanceVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request, cancellationToken);
    }
}
