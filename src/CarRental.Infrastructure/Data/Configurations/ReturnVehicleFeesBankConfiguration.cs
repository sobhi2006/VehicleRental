using CarRental.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for ReturnVehicleFeesBank.
/// </summary>
public class ReturnVehicleFeesBankConfiguration : IEntityTypeConfiguration<ReturnVehicleFeesBank>
{
    /// <summary>
    /// Configures the ReturnVehicleFeesBank entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<ReturnVehicleFeesBank> builder)
    {
        builder.ToTable("ReturnVehicleFeesBanks");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.HasIndex(e => new { e.ReturnVehicleId, e.FeesBankId }).IsUnique();

        builder.HasOne(e => e.ReturnVehicle)
            .WithMany(e => e.ReturnVehicleFeesBanks)
            .HasForeignKey(e => e.ReturnVehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.FeesBank)
            .WithMany(e => e.ReturnVehicleFeesBanks)
            .HasForeignKey(e => e.FeesBankId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}