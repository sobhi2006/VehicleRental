using CarRental.Application.Common;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;

namespace CarRental.Application.Services;

/// <summary>
/// Application service implementation for Invoice.
/// </summary>
public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceService"/> class.
    /// </summary>
    public InvoiceService(IInvoiceRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Creates a new Invoice.
    /// </summary>
    public async Task<Result<Invoice>> CreateAsync(Invoice entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Invoice>.Success(entity);
    }

    /// <summary>
    /// Updates an existing Invoice.
    /// </summary>
    public async Task<Result<Invoice>> UpdateAsync(Invoice entity, CancellationToken cancellationToken)
    {
        if (!await _repository.ExistsAsync(entity.Id, cancellationToken))
        {
            return Result<Invoice>.Failure("Invoice not found.");
        }

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Invoice>.Success(entity);
    }

    /// <summary>
    /// Deletes an existing Invoice.
    /// </summary>
    public async Task<Result> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result.Failure("Invoice not found.");
        }

        await _repository.DeleteAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    /// <summary>
    /// Gets a Invoice by id.
    /// </summary>
    public async Task<Result<Invoice>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<Invoice>.Failure("Invoice not found.");
        }

        return Result<Invoice>.Success(entity);
    }

    /// <summary>
    /// Gets all Invoices with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<Invoice>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<Invoice>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<Invoice>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var items = entities.ToList();
        var paginated = new PaginatedList<Invoice>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<Invoice>>.Success(paginated);
    }
}
