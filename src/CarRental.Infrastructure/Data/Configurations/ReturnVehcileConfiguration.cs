using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for ReturnVehicle.
/// </summary>
public class ReturnVehicleConfiguration : IEntityTypeConfiguration<ReturnVehicle>
{
    /// <summary>
    /// Configures the ReturnVehicle entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<ReturnVehicle> builder)
    {
        builder.ToTable("ReturnVehicles");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.BookingVehicle)
            .WithMany()
            .HasForeignKey(e => e.BookingId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.ConditionNotes).HasMaxLength(500);
        builder.Property(e => e.MileageAfter).HasPrecision(18, 2);
        builder.HasMany(e => e.ReturnVehicleFeesBanks)
            .WithOne(e => e.ReturnVehicle)
            .HasForeignKey(e => e.ReturnVehicleId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(e => e.DamageVehicle)
            .WithMany()
            .HasForeignKey(e => e.DamageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
