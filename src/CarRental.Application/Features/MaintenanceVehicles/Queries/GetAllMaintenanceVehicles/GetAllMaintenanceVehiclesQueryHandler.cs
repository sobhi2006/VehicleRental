using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.MaintenanceVehicles.Queries.GetAllMaintenanceVehicles;

/// <summary>
/// Handles retrieving all MaintenanceVehicles.
/// </summary>
public class GetAllMaintenanceVehiclesQueryHandler : IRequestHandler<GetAllMaintenanceVehiclesQuery, Result<PaginatedList<MaintenanceVehicleDto>>>
{
    private readonly IMaintenanceVehicleService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllMaintenanceVehiclesQueryHandler"/> class.
    /// </summary>
    public GetAllMaintenanceVehiclesQueryHandler(IMaintenanceVehicleService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<MaintenanceVehicleDto>>> Handle(GetAllMaintenanceVehiclesQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<MaintenanceVehicleDto>(value));
    }
}
