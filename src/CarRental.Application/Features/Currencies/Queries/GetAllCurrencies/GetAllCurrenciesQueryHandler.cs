using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Currencies.Queries.GetAllCurrencies;

/// <summary>
/// Handles retrieving all Currencies.
/// </summary>
public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, Result<PaginatedList<CurrencyDto>>>
{
    private readonly ICurrencyService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCurrenciesQueryHandler"/> class.
    /// </summary>
    public GetAllCurrenciesQueryHandler(ICurrencyService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<CurrencyDto>>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
