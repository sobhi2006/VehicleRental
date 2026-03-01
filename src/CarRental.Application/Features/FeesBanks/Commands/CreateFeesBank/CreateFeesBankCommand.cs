using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;

namespace CarRental.Application.Features.FeesBanks.Commands.CreateFeesBank;

/// <summary>
/// Command to create a FeesBank.
/// </summary>
public record CreateFeesBankCommand : IRequest<Result<FeesBankDto>>
{
    /// <summary>Gets or sets the Name.</summary>
    public string Name { get; init; } = string.Empty;
    /// <summary>Gets or sets the Amount.</summary>
    public decimal Amount { get; init; }
}
