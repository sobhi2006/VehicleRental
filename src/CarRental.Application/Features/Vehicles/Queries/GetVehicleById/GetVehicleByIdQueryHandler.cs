using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Vehicles.Queries.GetVehicleById;

/// <summary>
/// Handles retrieving a Vehicle by id.
/// </summary>
public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, Result<VehicleDto>>
{
    private readonly IVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetVehicleByIdQueryHandler"/> class.
    /// </summary>
    public GetVehicleByIdQueryHandler(IVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<VehicleDto>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<VehicleDto>(value));
    }
}
