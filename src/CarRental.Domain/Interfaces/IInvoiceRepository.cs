using CarRental.Domain.Entities;

namespace CarRental.Domain.Interfaces;

/// <summary>
/// Repository contract for Invoice.
/// </summary>
public interface IInvoiceRepository : IRepository<Invoice>
{
	Task DeleteLinesByInvoiceIdAsync(long invoiceId, CancellationToken cancellationToken);
	Task<Invoice?> GetInvoiceByBookingIdAsync(long bookingId, CancellationToken cancellationToken);
	Task<bool> ExistsActiveByBookingIdAsync(long bookingId, CancellationToken cancellationToken);
	Task<bool> ExistsActiveByBookingIdExcludeSelfAsync(long id, long bookingId, CancellationToken cancellationToken);
}
