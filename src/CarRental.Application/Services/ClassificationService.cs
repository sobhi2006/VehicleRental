using CarRental.Application.Common;
using CarRental.Application.Features.Classifications.Commands.CreateClassification;
using CarRental.Application.Features.Classifications.Commands.UpdateClassification;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities.Vehicles;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Classification.
/// </summary>
public class ClassificationService : IClassificationService
{
    private readonly IClassificationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClassificationService"/> class.
    /// </summary>
    public ClassificationService(IClassificationRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new Classification.
    /// </summary>
    public async Task<Result<Classification>> CreateAsync(Classification request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing Classification.
    /// </summary>
    public async Task<Result<Classification>> UpdateAsync(Classification request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<Classification>.Failure("Classification not found.");
        }

        entity.Name = request.Name;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing Classification.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Classification not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Classification by id.
    /// </summary>
    public async Task<Result<Classification>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Classification>.Failure("Classification not found.");
        }

        return entity;
    }

    /// <summary>
    /// Checks whether a Classification exists by identifier.
    /// </summary>
    public Task<bool> ExistsAsync(long id, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(id, cancellationToken);
    }

    /// <summary>
    /// Gets all Classifications with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Classification>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<Classification>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<Classification>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<Classification>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Classification>>.Success(paginated);
    }

    /// <summary>
    /// Checks if a Classification with the given Name already exists.
    /// </summary>
    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(c => string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase), cancellationToken);
    }

    /// <summary>
    /// Checks if a Classification with the given Name already exists, excluding the current Classification.
    /// </summary>
    public Task<bool> ExistsByNameExcludeSelfAsync(UpdateClassificationCommand request, CancellationToken cancellationToken)
    {
        return _repository.ExistsExcludeSelfAsync(
            request.Id,
            c => string.Equals(c.Name, request.Name, StringComparison.OrdinalIgnoreCase),
            cancellationToken);
    }
}
