using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Make.
/// </summary>
public class MakeConfiguration : IEntityTypeConfiguration<Make>
{
    /// <summary>
    /// Configures the Make entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Make> builder)
    {
        builder.ToTable("Makes");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.Name).IsRequired().HasMaxLength(500);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
