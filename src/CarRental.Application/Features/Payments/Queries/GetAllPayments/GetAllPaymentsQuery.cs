using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;

namespace CarRental.Application.Features.Payments.Queries.GetAllPayments;

/// <summary>
/// Query to get all Payments with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllPaymentsQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<PaymentDto>>>;
