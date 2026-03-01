using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Classification;

namespace CarRental.Application.Features.Classifications.Queries.GetAllClassifications;

/// <summary>
/// Query to get all Classifications with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllClassificationsQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<ClassificationDto>>>;
