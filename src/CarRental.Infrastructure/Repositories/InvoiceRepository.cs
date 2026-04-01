using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
using CarRental.Domain.Enums;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;

namespace CarRental.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for Invoice.
/// </summary>
public class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
    /// </summary>
    public InvoiceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public override async Task<Invoice?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.InvoiceLines)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public override async Task<IReadOnlyList<Invoice>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(e => e.InvoiceLines)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public Task DeleteLinesByInvoiceIdAsync(long invoiceId, CancellationToken cancellationToken)
    {
        return _context.InvoiceLines
            .Where(line => line.InvoiceId == invoiceId)
            .ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<Invoice?> GetInvoiceByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(e => e.InvoiceLines)
            .Where(e => e.BookingId == bookingId && e.Status != InvoiceStatus.Cancelled)
            .OrderByDescending(e => e.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<bool> ExistsActiveByBookingIdAsync(long bookingId, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(e => e.BookingId == bookingId && e.Status != InvoiceStatus.Cancelled, cancellationToken);
    }

    public Task<bool> ExistsActiveByBookingIdExcludeSelfAsync(long id, long bookingId, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(
            e => e.Id != id && e.BookingId == bookingId && e.Status != InvoiceStatus.Cancelled,
            cancellationToken);
    }

    // public override async Task<Invoice?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
