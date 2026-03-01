using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for BookingVehicle.
/// </summary>
public class BookingVehicleConfiguration : IEntityTypeConfiguration<BookingVehicle>
{
    /// <summary>
    /// Configures the BookingVehicle entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<BookingVehicle> builder)
    {
        builder.ToTable("BookingVehicles");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Driver)
            .WithMany()
            .HasForeignKey(e => e.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Vehicle)
            .WithMany()
            .HasForeignKey(e => e.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.Notes).HasMaxLength(500);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
