using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for BlockListCustomer.
/// </summary>
public class BlockListCustomerConfiguration : IEntityTypeConfiguration<BlockListCustomer>
{
    /// <summary>
    /// Configures the BlockListCustomer entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<BlockListCustomer> builder)
    {
        builder.ToTable("BlockListCustomers");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.HasOne(e => e.Driver)
            .WithMany()
            .HasForeignKey(e => e.DriverId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(500);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
