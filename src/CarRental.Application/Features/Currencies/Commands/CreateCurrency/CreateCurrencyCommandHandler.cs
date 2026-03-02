using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Currencies.Commands.CreateCurrency;

/// <summary>
/// Handles creation of Currency.
/// </summary>
public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Result<CurrencyDto>>
{
    private readonly ICurrencyService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateCurrencyCommandHandler"/> class.
    /// </summary>
    public CreateCurrencyCommandHandler(ICurrencyService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<CurrencyDto>> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request, cancellationToken);
    }
}
