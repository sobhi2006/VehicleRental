using CarRental.Application.Services;
using CarRental.Application.Interfaces;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using CarRental.Application.Common.Behaviors;

namespace CarRental.Application;

/// <summary>
/// Application layer dependency injection registration.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application services, MediatR, validators, and behaviors.
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);
        services.AddAutoMapper(assembly);

        services.Scan(scan => scan
            .FromAssemblies(assembly)
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
            .AsMatchingInterface()
            .WithScopedLifetime());

        services.AddScoped<IMakeService, MakeService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IClassificationService, ClassificationService>();
        services.AddScoped<IDriverService, DriverService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IFeesBankService, FeesBankService>();
        services.AddScoped<IBookingVehicleService, BookingVehicleService>();
        services.AddScoped<IDamageVehicleService, DamageVehicleService>();
        services.AddScoped<IMaintenanceVehicleService, MaintenanceVehicleService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        return services;
    }
}
