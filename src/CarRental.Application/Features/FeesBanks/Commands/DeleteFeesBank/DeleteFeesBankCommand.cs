using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.FeesBanks.Commands.DeleteFeesBank;

/// <summary>
/// Command to delete a FeesBank.
/// </summary>
/// <param name="Id">Identifier of the FeesBank.</param>
public record DeleteFeesBankCommand(long Id) : IRequest<Result>;
