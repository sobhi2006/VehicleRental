using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Vehicles.Commands.DeleteVehicle;

/// <summary>
/// Handles deletion of Vehicle.
/// </summary>
public class DeleteVehicleCommandHandler : IRequestHandler<DeleteVehicleCommand, Result>
{
    private readonly IVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteVehicleCommandHandler"/> class.
    /// </summary>
    public DeleteVehicleCommandHandler(IVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
