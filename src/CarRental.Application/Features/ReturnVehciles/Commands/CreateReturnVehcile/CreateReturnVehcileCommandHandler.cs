using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.ReturnVehicles.Commands.CreateReturnVehicle;

/// <summary>
/// Handles creation of ReturnVehicle.
/// </summary>
public class CreateReturnVehicleCommandHandler : IRequestHandler<CreateReturnVehicleCommand, Result<ReturnVehicleDto>>
{
    private readonly IReturnVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateReturnVehicleCommandHandler"/> class.
    /// </summary>
    public CreateReturnVehicleCommandHandler(IReturnVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<ReturnVehicleDto>> Handle(CreateReturnVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ReturnVehicle>(request);
        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<ReturnVehicleDto>(value));
    }
}
