using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for FeesBank.
/// </summary>
public class FeesBankConfiguration : IEntityTypeConfiguration<FeesBank>
{
    /// <summary>
    /// Configures the FeesBank entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<FeesBank> builder)
    {
        builder.ToTable("FeesBanks");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.Name).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Amount).HasPrecision(18, 2);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
