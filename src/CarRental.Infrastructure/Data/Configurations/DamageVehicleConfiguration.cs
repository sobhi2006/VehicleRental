using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for DamageVehicle.
/// </summary>
public class DamageVehicleConfiguration : IEntityTypeConfiguration<DamageVehicle>
{
    /// <summary>
    /// Configures the DamageVehicle entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<DamageVehicle> builder)
    {
        builder.ToTable("DamageVehicles");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Vehicle)
            .WithMany()
            .HasForeignKey(e => e.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.BookingVehicle)
            .WithMany()
            .HasForeignKey(e => e.BookingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(500);
        builder.Property(e => e.RepairCost).HasPrecision(18, 2);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
