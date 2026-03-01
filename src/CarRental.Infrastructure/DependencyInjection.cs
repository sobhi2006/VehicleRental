using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Repositories;

namespace CarRental.Infrastructure;

/// <summary>
/// Infrastructure layer dependency injection registration.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services and database context.
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:DefaultConnection"];

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IMakeRepository, MakeRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IClassificationRepository, ClassificationRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IFeesBankRepository, FeesBankRepository>();
        services.AddScoped<IBookingVehicleRepository, BookingVehicleRepository>();
        services.AddScoped<IDamageVehicleRepository, DamageVehicleRepository>();
        services.AddScoped<IMaintenanceVehicleRepository, MaintenanceVehicleRepository>();
        return services;
    }
}
