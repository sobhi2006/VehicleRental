using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.ReturnVehicles.Commands.UpdateReturnVehicle;

/// <summary>
/// Handles updates of ReturnVehicle.
/// </summary>
public class UpdateReturnVehicleCommandHandler : IRequestHandler<UpdateReturnVehicleCommand, Result<ReturnVehicleDto>>
{
    private readonly IReturnVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateReturnVehicleCommandHandler"/> class.
    /// </summary>
    public UpdateReturnVehicleCommandHandler(IReturnVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<ReturnVehicleDto>> Handle(UpdateReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(request, cancellationToken);
    }
}
