using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.ReturnVehicles.Commands.CreateReturnVehicle;

/// <summary>
/// Handles creation of ReturnVehicle.
/// </summary>
public class CreateReturnVehicleCommandHandler : IRequestHandler<CreateReturnVehicleCommand, Result<ReturnVehicleDto>>
{
    private readonly IReturnVehicleService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReturnVehicleCommandHandler"/> class.
    /// </summary>
    public CreateReturnVehicleCommandHandler(IReturnVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<ReturnVehicleDto>> Handle(CreateReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request, cancellationToken);
    }
}
