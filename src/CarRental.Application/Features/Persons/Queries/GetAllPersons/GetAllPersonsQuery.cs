using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;

namespace CarRental.Application.Features.Persons.Queries.GetAllPersons;

/// <summary>
/// Query to get all Persons with pagination.
/// </summary>
/// <param name="PageNumber">Page number (1-based).</param>
/// <param name="PageSize">Number of items per page.</param>
public record GetAllPersonsQuery(int PageNumber = 1, int PageSize = 10)
    : IRequest<Result<PaginatedList<PersonDto>>>;
