using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;

namespace CarRental.Application.Features.MaintenanceVehicles.Queries.GetAllMaintenanceVehicles;

/// <summary>
/// Query to get all MaintenanceVehicles with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllMaintenanceVehiclesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<MaintenanceVehicleDto>>>;
