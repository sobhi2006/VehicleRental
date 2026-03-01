using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Make;

namespace CarRental.Application.Features.Makes.Queries.GetMakeById;

/// <summary>
/// Query to get a Make by id.
/// </summary>
/// <param name="Id">Identifier of the Make.</param>
public record GetMakeByIdQuery(long Id) : IRequest<Result<MakeDto>>;
