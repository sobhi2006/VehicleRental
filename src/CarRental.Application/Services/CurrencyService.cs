using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Currency.
/// </summary>
public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrencyService"/> class.
    /// </summary>
    public CurrencyService(ICurrencyRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new Currency.
    /// </summary>
    public async Task<Result<Currency>> CreateAsync(Currency request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request;
    }

    /// <summary>
    /// Updates an existing Currency.
    /// </summary>
    public async Task<Result<Currency>> UpdateAsync(Currency request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<Currency>.Failure("Currency not found.");
        }

        entity.Name = request.Name;
        entity.ValueVsOneDollar = request.ValueVsOneDollar;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity;
    }

    /// <summary>
    /// Deletes an existing Currency.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Currency not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Currency by id.
    /// </summary>
    public async Task<Result<Currency>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Currency>.Failure("Currency not found.");
        }

        return entity;
    }

    /// <summary>
    /// Gets all Currencies with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Currency>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<Currency>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<Currency>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var items = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<Currency>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Currency>>.Success(paginated);
    }
    /// <summary>
    /// Check if the Currency Name is found. 
    /// </summary>
    public async Task<bool> ExistByNameAsync(string name, CancellationToken ct)
    {
        var NormalizedName = name.ToUpper();
        return await _repository.ExistsAsync(c => c.Name.ToUpper() == NormalizedName, ct);
    }
    /// <summary>
    /// Check if the Currency Name is found for other Id.
    /// </summary>
    public async Task<bool> ExistByNameExcludeSelfAsync(long id, string name, CancellationToken ct)
    {
        var NormalizedName = name.ToUpper();
        return await _repository.ExistsExcludeSelfAsync(id, c => c.Name.ToUpper() == NormalizedName, ct);
    }

    public Task<bool> ExistsByIdAsync(long id, CancellationToken cancellationToken)
    {
        return _repository.ExistsAsync(c => c.Id == id, cancellationToken);
    }
}
