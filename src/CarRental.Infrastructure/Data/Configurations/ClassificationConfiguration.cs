using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Classification.
/// </summary>
public class ClassificationConfiguration : IEntityTypeConfiguration<Classification>
{
    /// <summary>
    /// Configures the Classification entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Classification> builder)
    {
        builder.ToTable("Classifications");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.Name).IsRequired().HasMaxLength(500);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
