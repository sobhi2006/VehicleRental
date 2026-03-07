using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Vehicle.
/// </summary>
public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    /// <summary>
    /// Configures the Vehicle entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Make)
            .WithMany()
            .HasForeignKey(e => e.MakeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.VIN).IsRequired().HasMaxLength(500);
        builder.HasIndex(e => e.VIN).IsUnique();
        builder.Property(e => e.PlateNumber).IsRequired().HasMaxLength(500);
        builder.HasIndex(e => e.PlateNumber).IsUnique();
        builder.HasOne(e => e.Classification)
            .WithMany()
            .HasForeignKey(e => e.ClassificationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.CurrentMileage).HasPrecision(18, 2);

    }
}
