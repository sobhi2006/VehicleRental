using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for MaintenanceVehicle.
/// </summary>
public class MaintenanceVehicleConfiguration : IEntityTypeConfiguration<MaintenanceVehicle>
{
    /// <summary>
    /// Configures the MaintenanceVehicle entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<MaintenanceVehicle> builder)
    {
        builder.ToTable("MaintenanceVehicles");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Vehicle)
            .WithMany()
            .HasForeignKey(e => e.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.Cost).HasPrecision(18, 2);
        builder.Property(e => e.Notes).IsRequired().HasMaxLength(500);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
