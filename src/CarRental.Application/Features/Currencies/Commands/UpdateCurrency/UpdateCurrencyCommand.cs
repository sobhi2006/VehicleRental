using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;

namespace CarRental.Application.Features.Currencies.Commands.UpdateCurrency;

/// <summary>
/// Command to update a Currency.
/// </summary>
public record UpdateCurrencyCommand : IRequest<Result<CurrencyDto>>
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the ValueVsOneDollar.</summary>
    public decimal ValueVsOneDollar { get; init; }
}
