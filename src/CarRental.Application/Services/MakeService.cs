using CarRental.Application.Common;
using CarRental.Application.Features.Makes.Commands.CreateMake;
using CarRental.Application.Features.Makes.Commands.UpdateMake;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Make.
/// </summary>
public class MakeService : IMakeService
{
    private readonly IMakeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="MakeService"/> class.
    /// </summary>
    public MakeService(IMakeRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new Make.
    /// </summary>
    public async Task<Result<Make>> CreateAsync(Make request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing Make.
    /// </summary>
    public async Task<Result<Make>> UpdateAsync(Make request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<Make>.Failure("Make not found.");
        }

        entity.Name = request.Name;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing Make.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Make not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Make by id.
    /// </summary>
    public async Task<Result<Make>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Make>.Failure("Make not found.");
        }

        return entity;
    }

    /// <summary>
    /// Checks whether a Make exists by identifier.
    /// </summary>
    public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(id, cancellationToken);
    }

    /// <summary>
    /// Gets all Makes with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Make>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            return Result<PaginatedList<Make>>.Failure("PageNumber and PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<Make>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Make>>.Success(paginated);
    }

    /// <summary>
    /// Checks if a Make with the given Name already exists.
    /// </summary>
    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(m => string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase), cancellationToken);
    }

    /// <summary>
    /// Checks if a Make with the given Name already exists, excluding the current Make.
    /// </summary>
    public Task<bool> ExistsByNameExcludeSelfAsync(UpdateMakeCommand request, CancellationToken cancellationToken)
    {
        return _repository.ExistsExcludeSelfAsync(
            request.Id,
            m => string.Equals(m.Name, request.Name, StringComparison.OrdinalIgnoreCase),
            cancellationToken);
    }
}
