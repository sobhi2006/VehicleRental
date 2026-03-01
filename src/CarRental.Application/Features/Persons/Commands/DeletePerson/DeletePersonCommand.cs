using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.Persons.Commands.DeletePerson;

/// <summary>
/// Command to delete a Person.
/// </summary>
/// <param name="Id">Identifier of the Person.</param>
public record DeletePersonCommand(long Id) : IRequest<Result>;
