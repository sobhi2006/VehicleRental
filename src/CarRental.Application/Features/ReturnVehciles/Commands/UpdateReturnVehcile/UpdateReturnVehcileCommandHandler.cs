using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.ReturnVehicles.Commands.UpdateReturnVehicle;

/// <summary>
/// Handles updates of ReturnVehicle.
/// </summary>
public class UpdateReturnVehicleCommandHandler : IRequestHandler<UpdateReturnVehicleCommand, Result<ReturnVehicleDto>>
{
    private readonly IReturnVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateReturnVehicleCommandHandler"/> class.
    /// </summary>
    public UpdateReturnVehicleCommandHandler(IReturnVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<ReturnVehicleDto>> Handle(UpdateReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ReturnVehicle>(request);
        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<ReturnVehicleDto>(value));
    }
}
