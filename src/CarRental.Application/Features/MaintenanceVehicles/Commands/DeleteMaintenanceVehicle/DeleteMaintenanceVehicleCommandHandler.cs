using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.MaintenanceVehicles.Commands.DeleteMaintenanceVehicle;

/// <summary>
/// Handles deletion of MaintenanceVehicle.
/// </summary>
public class DeleteMaintenanceVehicleCommandHandler : IRequestHandler<DeleteMaintenanceVehicleCommand, Result>
{
    private readonly IMaintenanceVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMaintenanceVehicleCommandHandler"/> class.
    /// </summary>
    public DeleteMaintenanceVehicleCommandHandler(IMaintenanceVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteMaintenanceVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
