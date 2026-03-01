using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Pricing.
/// </summary>
public class PricingConfiguration : IEntityTypeConfiguration<Pricing>
{
    /// <summary>
    /// Configures the Pricing entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Pricing> builder)
    {
        builder.ToTable("Pricings");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Classification)
            .WithOne(e => e.Pricing)
            .HasForeignKey<Pricing>(e => e.ClassificationId)
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.Property(e => e.PaymentPerDay).HasPrecision(18, 2);
        builder.Property(e => e.CostPerExKm).HasPrecision(18, 2);
        builder.Property(e => e.CostPerLateDay).HasPrecision(18, 2);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
