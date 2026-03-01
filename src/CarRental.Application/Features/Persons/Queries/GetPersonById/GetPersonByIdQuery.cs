using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;

namespace CarRental.Application.Features.Persons.Queries.GetPersonById;

/// <summary>
/// Query to get a Person by id.
/// </summary>
/// <param name="Id">Identifier of the Person.</param>
public record GetPersonByIdQuery(long Id) : IRequest<Result<PersonDto>>;
