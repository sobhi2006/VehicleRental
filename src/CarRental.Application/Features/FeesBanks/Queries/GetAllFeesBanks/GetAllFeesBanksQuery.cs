using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;

namespace CarRental.Application.Features.FeesBanks.Queries.GetAllFeesBanks;

/// <summary>
/// Query to get all FeesBanks with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllFeesBanksQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<FeesBankDto>>>;
