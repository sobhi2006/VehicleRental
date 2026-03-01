using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.DamageVehicle;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Application.Features.DamageVehicles.Commands.CreateDamageVehicle;

/// <summary>
/// Handles creation of DamageVehicle.
/// </summary>
public class CreateDamageVehicleCommandHandler : IRequestHandler<CreateDamageVehicleCommand, Result<DamageVehicleDto>>
{
    private readonly IDamageVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateDamageVehicleCommandHandler"/> class.
    /// </summary>
    public CreateDamageVehicleCommandHandler(IDamageVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<DamageVehicleDto>> Handle(CreateDamageVehicleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DamageVehicle>(request);

        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<DamageVehicleDto>(value));
    }
}
