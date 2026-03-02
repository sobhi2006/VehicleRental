using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.Currencies.Commands.DeleteCurrency;

/// <summary>
/// Command to delete a Currency.
/// </summary>
/// <param name="Id">Identifier of the Currency.</param>
public record DeleteCurrencyCommand(long Id) : IRequest<Result>;
