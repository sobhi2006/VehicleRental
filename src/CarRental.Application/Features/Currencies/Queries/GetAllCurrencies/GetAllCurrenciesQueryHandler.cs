using MediatR;
using AutoMapper;
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
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCurrenciesQueryHandler"/> class.
    /// </summary>
    public GetAllCurrenciesQueryHandler(ICurrencyService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<CurrencyDto>>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<CurrencyDto>(value));
    }
}
