using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;

namespace CarRental.Application.Features.Vehicles.Queries.GetAllVehicles;

/// <summary>
/// Query to get all Vehicles with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllVehiclesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<VehicleDto>>>;
