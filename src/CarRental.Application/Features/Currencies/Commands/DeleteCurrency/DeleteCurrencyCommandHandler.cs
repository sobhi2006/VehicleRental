using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Currencies.Commands.DeleteCurrency;

/// <summary>
/// Handles deletion of Currency.
/// </summary>
public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Result>
{
    private readonly ICurrencyService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteCurrencyCommandHandler"/> class.
    /// </summary>
    public DeleteCurrencyCommandHandler(ICurrencyService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
