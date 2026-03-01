using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BookingVehicle;

namespace CarRental.Application.Features.BookingVehicles.Queries.GetAllBookingVehicles;

/// <summary>
/// Query to get all BookingVehicles with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllBookingVehiclesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<BookingVehicleDto>>>;
