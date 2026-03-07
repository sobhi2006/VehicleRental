using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Common;
using CarRental.Domain.Entities.Vehicles;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using CarRental.Domain.Entities.ImageEntities;

namespace CarRental.Infrastructure.Data;

/// <summary>
/// EF Core database context for the application.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public DbSet<DamageVehicleImage> DamageVehicleImages => Set<DamageVehicleImage>();
    public DbSet<VehicleImage> VehicleImages => Set<VehicleImage>();
    public DbSet<BlockListCustomer> BlockListCustomers => Set<BlockListCustomer>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLine> InvoiceLines => Set<InvoiceLine>();
    public DbSet<ReturnVehicle> ReturnVehicles => Set<ReturnVehicle>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<Pricing> Pricing => Set<Pricing>();
    public DbSet<MaintenanceVehicle> MaintenanceVehicles => Set<MaintenanceVehicle>();
    public DbSet<DamageVehicle> DamageVehicles => Set<DamageVehicle>();
    public DbSet<BookingVehicle> BookingVehicles => Set<BookingVehicle>();
    public DbSet<FeesBank> FeesBanks => Set<FeesBank>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<Classification> Classifications => Set<Classification>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Make> Makes => Set<Make>();
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Saves changes and applies audit timestamps.
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Configures the model and applies entity configurations.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                var propertyType = property.ClrType;
                var enumType = Nullable.GetUnderlyingType(propertyType) ?? propertyType;

                if (!enumType.IsEnum)
                {
                    continue;
                }

                var converterType = typeof(EnumToStringConverter<>).MakeGenericType(enumType);
                var converter = (ValueConverter)Activator.CreateInstance(converterType)!;

                property.SetValueConverter(converter);
            }
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        /*
        Important Practical Note:
            Because ApplyConfigurationsFromAssembly(...) is called after your enum loop, 
            any explicit converter set in config classes could override this global converter for specific properties.
            Put the enum loop before ApplyConfigurationsFromAssembly(...) to ensure all enums use the string converter by default,
            and then override specific properties in config classes as needed.
        */
    }
}
