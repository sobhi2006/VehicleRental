using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.DamageVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.DamageVehicles.Queries.GetDamageVehicleById;

/// <summary>
/// Handles retrieving a DamageVehicle by id.
/// </summary>
public class GetDamageVehicleByIdQueryHandler : IRequestHandler<GetDamageVehicleByIdQuery, Result<DamageVehicleDto>>
{
    private readonly IDamageVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetDamageVehicleByIdQueryHandler"/> class.
    /// </summary>
    public GetDamageVehicleByIdQueryHandler(IDamageVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<DamageVehicleDto>> Handle(GetDamageVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<DamageVehicleDto>(value));
    }
}
