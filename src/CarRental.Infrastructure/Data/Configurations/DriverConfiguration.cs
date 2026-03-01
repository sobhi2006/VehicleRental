using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Driver.
/// </summary>
public class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    /// <summary>
    /// Configures the Driver entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("Drivers");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Person)
            .WithMany()
            .HasForeignKey(e => e.PersonId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.DriverLicenseNumber).IsRequired().HasMaxLength(500);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
