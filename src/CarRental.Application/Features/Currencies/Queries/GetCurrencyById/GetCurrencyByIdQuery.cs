using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;

namespace CarRental.Application.Features.Currencies.Queries.GetCurrencyById;

/// <summary>
/// Query to get a Currency by id.
/// </summary>
/// <param name="Id">Identifier of the Currency.</param>
public record GetCurrencyByIdQuery(long Id) : IRequest<Result<CurrencyDto>>;
