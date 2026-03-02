using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;
using CarRental.Application.Features.Currencies.Commands.CreateCurrency;
using CarRental.Application.Features.Currencies.Commands.UpdateCurrency;
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
    public async Task<Result<CurrencyDto>> CreateAsync(CreateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var entity = new Currency
        {
            Name = request.Name,
            ValueVsOneDollar = request.ValueVsOneDollar,
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
    }

    /// <summary>
    /// Updates an existing Currency.
    /// </summary>
    public async Task<Result<CurrencyDto>> UpdateAsync(UpdateCurrencyCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return Result<CurrencyDto>.Failure("Currency not found.");
        }

        entity.Name = request.Name;
        entity.ValueVsOneDollar = request.ValueVsOneDollar;

        await _repository.UpdateAsync(entity, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToDto(entity);
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
    public async Task<Result<CurrencyDto>> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return Result<CurrencyDto>.Failure("Currency not found.");
        }

        return MapToDto(entity);
    }

    /// <summary>
    /// Gets all Currencies with pagination.
    /// </summary>
    public async Task<Result<PaginatedList<CurrencyDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<CurrencyDto>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<CurrencyDto>>.Failure("PageSize must be greater than 0.");
        }

        var totalCount = await _repository.CountAsync(cancellationToken);
        var entities = await _repository.GetPagedAsync(pageNumber, pageSize, cancellationToken);

        var items = entities
            .Select(MapToDto)
            .ToList();

        var paginated = new PaginatedList<CurrencyDto>(items, totalCount, pageNumber, pageSize);

        return Result<PaginatedList<CurrencyDto>>.Success(paginated);
    }

    /// <summary>
    /// Maps a domain entity to a DTO.
    /// </summary>
    private static CurrencyDto MapToDto(Currency entity)
    {
        return new CurrencyDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ValueVsOneDollar = entity.ValueVsOneDollar,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
