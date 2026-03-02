using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;

namespace CarRental.Application.Features.Currencies.Queries.GetAllCurrencies;

/// <summary>
/// Query to get all Currencies with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllCurrenciesQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<CurrencyDto>>>;
