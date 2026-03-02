using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;

namespace CarRental.Application.Features.Currencies.Commands.CreateCurrency;

/// <summary>
/// Command to create a Currency.
/// </summary>
public record CreateCurrencyCommand : IRequest<Result<CurrencyDto>>
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the ValueVsOneDollar.</summary>
    public decimal ValueVsOneDollar { get; init; }
}
