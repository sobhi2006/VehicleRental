using CarRental.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using CarRental.Domain.Common;
using CarRental.Domain.Entities.Vehicles;

namespace CarRental.Infrastructure.Data;

/// <summary>
/// EF Core database context for the application.
/// </summary>
public class ApplicationDbContext : DbContext
{
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
