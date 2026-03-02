using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.ReturnVehicles.Commands.DeleteReturnVehicle;

/// <summary>
/// Handles deletion of ReturnVehicle.
/// </summary>
public class DeleteReturnVehicleCommandHandler : IRequestHandler<DeleteReturnVehicleCommand, Result>
{
    private readonly IReturnVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteReturnVehicleCommandHandler"/> class.
    /// </summary>
    public DeleteReturnVehicleCommandHandler(IReturnVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
