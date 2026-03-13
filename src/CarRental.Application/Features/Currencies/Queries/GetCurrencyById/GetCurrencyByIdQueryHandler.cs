using MediatR;
using AutoMapper;
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
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCurrencyByIdQueryHandler"/> class.
    /// </summary>
    public GetCurrencyByIdQueryHandler(ICurrencyService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<CurrencyDto>> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<CurrencyDto>(value));
    }
}
