using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Make;

namespace CarRental.Application.Features.Makes.Queries.GetAllMakes;

/// <summary>
/// Query to get all Makes with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllMakesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<MakeDto>>>;
