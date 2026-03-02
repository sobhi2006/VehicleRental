using CarRental.Application.Common;
using CarRental.Application.DTOs.Currency;
using CarRental.Application.Features.Currencies.Commands.CreateCurrency;
using CarRental.Application.Features.Currencies.Commands.UpdateCurrency;

namespace CarRental.Application.Interfaces;

/// <summary>
/// Application service contract for Currency operations.
/// </summary>
public interface ICurrencyService
{
    /// <summary>
    /// Creates a new Currency.
    /// </summary>
    Task<Result<CurrencyDto>> CreateAsync(CreateCurrencyCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing Currency.
    /// </summary>
    Task<Result<CurrencyDto>> UpdateAsync(UpdateCurrencyCommand request, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes an existing Currency.
    /// </summary>
    Task<Result> DeleteAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a Currency by id.
    /// </summary>
    Task<Result<CurrencyDto>> GetByIdAsync(long id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all Currencies with pagination.
    /// </summary>
    Task<Result<PaginatedList<CurrencyDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
