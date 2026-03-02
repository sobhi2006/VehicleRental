using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for InvoiceLine.
/// </summary>
public class InvoiceLineService : IInvoiceLineService
{
    private readonly IInvoiceLineRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceLineService"/> class.
    /// </summary>
    public InvoiceLineService(IInvoiceLineRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new InvoiceLine.
    /// </summary>
    public async Task<Result<InvoiceLine>> CreateAsync(InvoiceLine request, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(request, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<InvoiceLine>.Success(request);
    }

    /// <summary>
    /// Updates an existing InvoiceLine.
    /// </summary>
    public async Task<Result<InvoiceLine>> UpdateAsync(InvoiceLine request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<InvoiceLine>.Failure("InvoiceLine not found.");
        }

        entity.InvoiceId = request.InvoiceId;
        entity.Description = request.Description;
        entity.Quantity = request.Quantity;
        entity.UnitPrice = request.UnitPrice;
        entity.LineTotal = request.LineTotal;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<InvoiceLine>.Success(entity);
    }

    /// <summary>
    /// Deletes an existing InvoiceLine.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("InvoiceLine not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a InvoiceLine by id.
    /// </summary>
    public async Task<Result<InvoiceLine>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<InvoiceLine>.Failure("InvoiceLine not found.");
        }

        return Result<InvoiceLine>.Success(entity);
    }

    /// <summary>
    /// Gets all InvoiceLines with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<InvoiceLine>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<InvoiceLine>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<InvoiceLine>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);
        var paginated = new PaginatedList<InvoiceLine>(entities, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<InvoiceLine>>.Success(paginated);
    }
}
