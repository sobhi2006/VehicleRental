using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Currencies.Commands.UpdateCurrency;

/// <summary>
/// Handles updates of Currency.
/// </summary>
public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Result<CurrencyDto>>
{
    private readonly ICurrencyService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCurrencyCommandHandler"/> class.
    /// </summary>
    public UpdateCurrencyCommandHandler(ICurrencyService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<CurrencyDto>> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Currency>(request);
        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<CurrencyDto>(value));
    }
}
