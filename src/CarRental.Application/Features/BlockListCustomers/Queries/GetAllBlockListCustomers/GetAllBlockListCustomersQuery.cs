using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BlockListCustomer;

namespace CarRental.Application.Features.BlockListCustomers.Queries.GetAllBlockListCustomers;

/// <summary>
/// Query to get all BlockListCustomers with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllBlockListCustomersQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<BlockListCustomerDto>>>;
