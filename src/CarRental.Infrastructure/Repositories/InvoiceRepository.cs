using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Entities;
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

    // public override async Task<Invoice?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    // {
    //     return await _dbSet
    //         .Include(e => e.RelatedEntity)
    //         .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    // }
}
