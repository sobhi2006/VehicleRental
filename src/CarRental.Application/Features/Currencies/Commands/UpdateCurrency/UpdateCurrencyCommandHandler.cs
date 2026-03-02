using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Currencies.Commands.UpdateCurrency;

/// <summary>
/// Handles updates of Currency.
/// </summary>
public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Result<CurrencyDto>>
{
    private readonly ICurrencyService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateCurrencyCommandHandler"/> class.
    /// </summary>
    public UpdateCurrencyCommandHandler(ICurrencyService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<CurrencyDto>> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(request, cancellationToken);
    }
}
