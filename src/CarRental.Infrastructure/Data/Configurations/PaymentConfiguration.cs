using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Payment.
/// </summary>
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    /// <summary>
    /// Configures the Payment entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.BookingVehicle)
            .WithMany()
            .HasForeignKey(e => e.BookingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Currency)
            .WithMany()
            .HasForeignKey(e => e.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.Amount).HasPrecision(18, 2);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
