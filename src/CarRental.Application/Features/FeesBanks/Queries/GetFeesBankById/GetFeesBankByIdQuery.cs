using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;

namespace CarRental.Application.Features.FeesBanks.Queries.GetFeesBankById;

/// <summary>
/// Query to get a FeesBank by id.
/// </summary>
/// <param name="Id">Identifier of the FeesBank.</param>
public record GetFeesBankByIdQuery(long Id) : IRequest<Result<FeesBankDto>>;
