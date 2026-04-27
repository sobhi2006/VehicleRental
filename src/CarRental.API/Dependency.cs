using Asp.Versioning;
using CarRental.API.Authorization;
using CarRental.Application;
using CarRental.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CarRental.API;

/// <summary>
/// Registers API dependencies through a chained service registration pipeline.
/// </summary>
public static class Dependency
{
    /// <summary>
    /// Entry point for API service registration before building the application.
    /// </summary>
    public static IServiceCollection RegisterDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddApiControllers()
            .AddApiVersioningServices()
            .AddApiExplorers()
            .AddApiSwagger()
            .AddApplicationLayer()
            .AddInfrastructureLayer(configuration)
            .AddApiAuthorization();
    }

    public static IServiceCollection AddApiControllers(this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }

    public static IServiceCollection AddApiVersioningServices(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1.0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    public static IServiceCollection AddApiExplorers(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        return services;
    }

    public static IServiceCollection AddApiSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarRental API", Version = "v1" });
            c.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter JWT Bearer token."
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddApplication();
        return services;
    }

    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        return services;
    }

    public static IServiceCollection AddApiAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.ActiveUserPolicy, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new ActiveUserRequirement());
            });
        });

        services.AddSingleton<IAuthorizationHandler, ActiveUserRequirementHandler>();
        return services;
    }
}
