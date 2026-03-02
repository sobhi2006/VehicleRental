using CarRental.Application.Common;
using CarRental.Application.DTOs.InvoiceLine;
using CarRental.Application.Features.InvoiceLines.Commands.CreateInvoiceLine;
using CarRental.Application.Features.InvoiceLines.Commands.UpdateInvoiceLine;
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
    public async Task<Result<InvoiceLineDto>> CreateAsync(CreateInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        var entity = new InvoiceLine
        {
            InvoiceId = request.InvoiceId,
            Description = request.Description,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            LineTotal = request.LineTotal,
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
    }

    /// <summary>
    /// Updates an existing InvoiceLine.
    /// </summary>
    public async Task<Result<InvoiceLineDto>> UpdateAsync(UpdateInvoiceLineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<InvoiceLineDto>.Failure("InvoiceLine not found.");
        }

        entity.InvoiceId = request.InvoiceId;
        entity.Description = request.Description;
        entity.Quantity = request.Quantity;
        entity.UnitPrice = request.UnitPrice;
        entity.LineTotal = request.LineTotal;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
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
    public async Task<Result<InvoiceLineDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<InvoiceLineDto>.Failure("InvoiceLine not found.");
        }

        return MapToDto(entity);
    }

    /// <summary>
    /// Gets all InvoiceLines with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<InvoiceLineDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<InvoiceLineDto>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<InvoiceLineDto>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var items = entities
            .Select(MapToDto)
            .ToList();

        var paginated = new PaginatedList<InvoiceLineDto>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<InvoiceLineDto>>.Success(paginated);
    }

    /// <summary>
    /// Maps a domain entity to a DTO.
    /// </summary>
    private static InvoiceLineDto MapToDto(InvoiceLine entity)
    {
        return new InvoiceLineDto
        {
            Id = entity.Id,
            InvoiceId = entity.InvoiceId,
            Description = entity.Description,
            Quantity = entity.Quantity,
            UnitPrice = entity.UnitPrice,
            LineTotal = entity.LineTotal,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
