using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;

namespace CarRental.Application.Features.ReturnVehicles.Queries.GetAllReturnVehicles;

/// <summary>
/// Query to get all ReturnVehicles with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllReturnVehiclesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<ReturnVehicleDto>>>;
