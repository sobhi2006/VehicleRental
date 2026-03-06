using CarRental.Application.Common;
using CarRental.Application.Features.Drivers.Commands.CreateDriver;
using CarRental.Application.Features.Persons.Commands.CreatePerson;
using CarRental.Application.Features.Persons.Commands.UpdateNationalNo;
using CarRental.Application.Features.Persons.Commands.UpdatePerson;
using CarRental.Domain.Entities;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Person operations.
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Creates a new Person.
    /// </summary>
    Task<Result<Person>> CreateAsync(Person request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Person.
    /// </summary>
    Task<Result<Person>> UpdateAsync(Person request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Person.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Person by id.
    /// </summary>
    Task<Result<Person>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Persons with pagination.
    /// </summary>
    Task<Result<PaginatedList<Person>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Result<Person>> UpdateNationalNoAsync(long id, string nationalNo, CancellationToken cancellationToken);
    Task<bool> ExistsByNationalNoAsync(string nationalNo, CancellationToken cancellationToken);
    Task<bool> ExistsByNationalNoExcludeSelfAsync(UpdateNationalNoCommand request, CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(long personId, CancellationToken cancellation);
}
