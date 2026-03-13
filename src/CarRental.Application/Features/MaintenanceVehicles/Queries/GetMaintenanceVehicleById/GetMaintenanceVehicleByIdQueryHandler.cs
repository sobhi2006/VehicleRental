using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.MaintenanceVehicles.Queries.GetMaintenanceVehicleById;

/// <summary>
/// Handles retrieving a MaintenanceVehicle by id.
/// </summary>
public class GetMaintenanceVehicleByIdQueryHandler : IRequestHandler<GetMaintenanceVehicleByIdQuery, Result<MaintenanceVehicleDto>>
{
    private readonly IMaintenanceVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMaintenanceVehicleByIdQueryHandler"/> class.
    /// </summary>
    public GetMaintenanceVehicleByIdQueryHandler(IMaintenanceVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> Handle(GetMaintenanceVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<MaintenanceVehicleDto>(value));
    }
}
