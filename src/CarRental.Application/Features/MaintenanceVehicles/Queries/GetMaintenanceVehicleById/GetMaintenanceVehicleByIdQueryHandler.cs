using MediatR;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="GetMaintenanceVehicleByIdQueryHandler"/> class.
    /// </summary>
    public GetMaintenanceVehicleByIdQueryHandler(IMaintenanceVehicleService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<MaintenanceVehicleDto>> Handle(GetMaintenanceVehicleByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id, cancellationToken);
    }
}
