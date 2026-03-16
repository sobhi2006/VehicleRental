using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for BlockListCustomer.
/// </summary>
public class BlockListCustomerService : IBlockListCustomerService
{
    private readonly IBlockListCustomerRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockListCustomerService"/> class.
    /// </summary>
    public BlockListCustomerService(IBlockListCustomerRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new BlockListCustomer.
    /// </summary>
    public async Task<Result<BlockListCustomer>> CreateAsync(BlockListCustomer entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BlockListCustomer>.Success(entity);
    }

    /// <summary>
    /// Updates an existing BlockListCustomer.
    /// </summary>
    public async Task<Result<BlockListCustomer>> UpdateAsync(BlockListCustomer entity, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistsAsync(entity.Id, cancellationToken))
        {
            return Result<BlockListCustomer>.Failure("BlockListCustomer not found.");
        }

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<BlockListCustomer>.Success(entity);
    }

    /// <summary>
    /// Deletes an existing BlockListCustomer.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("BlockListCustomer not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a BlockListCustomer by id.
    /// </summary>
    public async Task<Result<BlockListCustomer>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<BlockListCustomer>.Failure("BlockListCustomer not found.");
        }

        return Result<BlockListCustomer>.Success(entity);
    }

    /// <summary>
    /// Gets all BlockListCustomers with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<BlockListCustomer>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<BlockListCustomer>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<BlockListCustomer>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var items = entities.ToList();
        var paginated = new PaginatedList<BlockListCustomer>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<BlockListCustomer>>.Success(paginated);
    }

    public async Task<bool> IsDriverBlockedById(long driverId, CancellationToken ct)
    {
        return await _repository.IsDriverBlockedByIdAsync(driverId, ct);
    }
}
