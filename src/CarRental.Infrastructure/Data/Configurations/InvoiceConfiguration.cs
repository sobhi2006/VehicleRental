using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Invoice.
/// </summary>
public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    /// <summary>
    /// Configures the Invoice entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.BookingVehicle)
            .WithOne()
            .HasForeignKey<Invoice>(e => e.BookingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.TotalAmount).HasPrecision(18, 2);
        builder.Property(e => e.PaidAmount).HasPrecision(18, 2);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
