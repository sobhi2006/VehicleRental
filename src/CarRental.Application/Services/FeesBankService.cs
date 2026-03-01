using CarRental.Application.Common;
using CarRental.Application.Features.FeesBanks.Commands.CreateFeesBank;
using CarRental.Application.Features.FeesBanks.Commands.UpdateFeesBank;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for FeesBank.
/// </summary>
public class FeesBankService : IFeesBankService
{
    private readonly IFeesBankRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="FeesBankService"/> class.
    /// </summary>
    public FeesBankService(IFeesBankRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new FeesBank.
    /// </summary>
    public async Task<Result<FeesBank>> CreateAsync(FeesBank request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing FeesBank.
    /// </summary>
    public async Task<Result<FeesBank>> UpdateAsync(FeesBank request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<FeesBank>.Failure("FeesBank not found.");
        }

        entity.Name = request.Name;
        entity.Amount = request.Amount;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing FeesBank.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("FeesBank not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a FeesBank by id.
    /// </summary>
    public async Task<Result<FeesBank>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<FeesBank>.Failure("FeesBank not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all FeesBanks with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<FeesBank>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<FeesBank>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<FeesBank>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<FeesBank>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<FeesBank>>.Success(paginated);
    }

    /// <summary>
    /// Checks if a FeesBank with the given Name already exists.
    /// </summary>
    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase), cancellationToken);
    }

    /// <summary>
    /// Checks if a FeesBank with the given Name already exists, excluding the current FeesBank.
    /// </summary>
    public Task<bool> ExistsByNameExcludeSelfAsync(UpdateFeesBankCommand request, CancellationToken cancellationToken)
    {
        return _repository.ExistsExcludeSelfAsync(
            request.Id,
            f => string.Equals(f.Name, request.Name, StringComparison.OrdinalIgnoreCase),
            cancellationToken);
    }
}
