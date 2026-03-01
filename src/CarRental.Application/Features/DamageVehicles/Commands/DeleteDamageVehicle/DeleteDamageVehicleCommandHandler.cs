using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.DamageVehicles.Commands.DeleteDamageVehicle;

/// <summary>
/// Handles deletion of DamageVehicle.
/// </summary>
public class DeleteDamageVehicleCommandHandler : IRequestHandler<DeleteDamageVehicleCommand, Result>
{
    private readonly IDamageVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDamageVehicleCommandHandler"/> class.
    /// </summary>
    public DeleteDamageVehicleCommandHandler(IDamageVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteDamageVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
