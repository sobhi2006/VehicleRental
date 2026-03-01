using CarRental.Application.Common;
using CarRental.Application.Features.Persons.Commands.CreatePerson;
using CarRental.Application.Features.Persons.Commands.UpdateNationalNo;
using CarRental.Application.Features.Persons.Commands.UpdatePerson;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Person.
/// </summary>
public class PersonService : IPersonService
{
    private readonly IPersonRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonService"/> class.
    /// </summary>
    public PersonService(IPersonRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new Person.
    /// </summary>
    public async Task<Result<Person>> CreateAsync(Person request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing Person.
    /// </summary>
    public async Task<Result<Person>> UpdateAsync(Person request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<Person>.Failure("Person not found.");
        }

        entity.FirstName = request.FirstName;
        entity.MiddleName = request.MiddleName;
        entity.LastName = request.LastName;
        entity.DateOfBirth = request.DateOfBirth;
        entity.Email = request.Email;
        entity.PhoneNumber = request.PhoneNumber;
        entity.Address = request.Address;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing Person.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Person not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Person by id.
    /// </summary>
    public async Task<Result<Person>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Person>.Failure("Person not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all Persons with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Person>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<Person>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<Person>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var paginated = new PaginatedList<Person>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Person>>.Success(paginated);
    }

    /// <summary>
    /// Updates the NationalNo of an existing Person.
    /// </summary>
    public async Task<Result<Person>> UpdateNationalNoAsync(long id, string nationalNo, CancellationToken cancellationToken)
    {
        var person = await _repository.GetByIdAsync(id, cancellationToken);
        if(person is null)
            return Result<Person>.Failure("Person not found.");

        person.NationalNo = nationalNo;

        await _repository.UpdateAsync(person, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return person;
    }

    /// <summary>
    /// Checks if a Person with the given NationalNo already exists.
    /// </summary>
    public async Task<bool> ExistsByNationalNoAsync(string nationalNo, CancellationToken cancellationToken)
    {
        return await _repository.ExistsAsync(
            p => string.Equals(p.NationalNo, nationalNo, StringComparison.OrdinalIgnoreCase),
            cancellationToken);
    }
    /// <summary>
    /// Checks if a Person with the given NationalNo already exists, excluding the current person.
    /// </summary>
    public Task<bool> ExistsByNationalNoExcludeSelfAsync(UpdateNationalNoCommand request, CancellationToken cancellationToken)
    {
        return _repository.ExistsExcludeSelfAsync(
            request.Id,
            p => string.Equals(p.NationalNo, request.NationalNo, StringComparison.OrdinalIgnoreCase),
            cancellationToken);
    }
}
