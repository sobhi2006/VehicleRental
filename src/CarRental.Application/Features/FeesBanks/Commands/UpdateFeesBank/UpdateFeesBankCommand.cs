using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;

namespace CarRental.Application.Features.FeesBanks.Commands.UpdateFeesBank;

/// <summary>
/// Command to update a FeesBank.
/// </summary>
public record UpdateFeesBankCommand : IRequest<Result<FeesBankDto>>
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; init; }
}
