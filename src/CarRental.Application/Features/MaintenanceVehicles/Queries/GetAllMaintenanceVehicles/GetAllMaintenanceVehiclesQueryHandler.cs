using MediatR;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllMaintenanceVehiclesQueryHandler"/> class.
    /// </summary>
    public GetAllMaintenanceVehiclesQueryHandler(IMaintenanceVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<MaintenanceVehicleDto>>> Handle(GetAllMaintenanceVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
