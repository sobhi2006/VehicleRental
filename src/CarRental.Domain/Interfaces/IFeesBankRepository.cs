using CarRental.Domain.Entities;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for FeesBank.
/// </summary>
public interface IFeesBankRepository : IRepository<FeesBank>
{
	Task<List<FeesBank>> GetByIdsAsync(IReadOnlyCollection<long> ids, CancellationToken cancellationToken);
	Task<int> CountByIdsAsync(IReadOnlyCollection<long> ids, CancellationToken cancellationToken);
}
