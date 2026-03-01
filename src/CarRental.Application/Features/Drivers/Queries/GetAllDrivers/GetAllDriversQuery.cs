using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Driver;

namespace CarRental.Application.Features.Drivers.Queries.GetAllDrivers;

/// <summary>
/// Query to get all Drivers with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllDriversQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<DriverDto>>>;
