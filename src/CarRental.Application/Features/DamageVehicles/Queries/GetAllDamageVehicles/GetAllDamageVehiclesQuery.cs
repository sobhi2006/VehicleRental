using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.DamageVehicle;

namespace CarRental.Application.Features.DamageVehicles.Queries.GetAllDamageVehicles;

/// <summary>
/// Query to get all DamageVehicles with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllDamageVehiclesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<DamageVehicleDto>>>;
