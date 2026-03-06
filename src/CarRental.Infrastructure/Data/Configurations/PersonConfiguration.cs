using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Data.Configurations;

/// <summary>
/// EF Core configuration for Person.
/// </summary>
public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    /// <summary>
    /// Configures the Person entity mapping.
    /// </summary>
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("Persons");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        
        builder.Property(e => e.FirstName).IsRequired().HasMaxLength(500);
        builder.Property(e => e.MiddleName).IsRequired(false).HasMaxLength(500);
        builder.Property(e => e.LastName).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Email).IsRequired().HasMaxLength(500);
        builder.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Address).IsRequired().HasMaxLength(500);

        builder.Property(e => e.CreatedAt).IsRequired();
    }
}
