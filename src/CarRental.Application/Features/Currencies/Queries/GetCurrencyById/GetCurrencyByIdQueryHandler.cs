using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Currencies.Queries.GetCurrencyById;

/// <summary>
/// Handles retrieving a Currency by id.
/// </summary>
public class GetCurrencyByIdQueryHandler : IRequestHandler<GetCurrencyByIdQuery, Result<CurrencyDto>>
{
    private readonly ICurrencyService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCurrencyByIdQueryHandler"/> class.
    /// </summary>
    public GetCurrencyByIdQueryHandler(ICurrencyService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<CurrencyDto>> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id, cancellationToken);
    }
}
