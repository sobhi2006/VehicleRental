using FluentValidation;
using CarRental.API;
using CarRental.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CarRental.Infrastructure.Identity;

/// <summary>
/// Application entry point.
/// </summary>
public partial class Program
{
    /// <summary>
    /// Configures and runs the web application.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.RegisterDependencies(builder.Configuration);

        var app = builder.Build();

        // Configure the HTTP request pipeline
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarRental API v1"));
        }

        // Global exception handler for validation errors
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature?.Error;

                if (exception is ValidationException validationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Response.ContentType = "application/json";

                    var errors = validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    await context.Response.WriteAsJsonAsync(new { errors });
                }
            });
        });

        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (app.Environment.IsEnvironment("Testing"))
            {
                await db.Database.EnsureCreatedAsync();
            }
            else
            {
                await db.Database.MigrateAsync();
            }

            await IdentitySeeder.SeedAsync(scope.ServiceProvider);
        }

        app.MapGet("/", () =>
        {
            var Endpoints = app.Services.GetRequiredService<EndpointDataSource>().Endpoints;
            var NumberOfEndpoints = Endpoints.Count - 1;
            var NamesOfEndpoints = Endpoints.Select(e => e.DisplayName).Where(name => !string.IsNullOrEmpty(name)).ToList();
            return $"\t\t\t\t\t\t\t\t\t\t\t\tThe Number Of Endpoints In This Project Is {NumberOfEndpoints}\n\n" +
                   $"Endpoints:\n{string.Join("\n", NamesOfEndpoints)}";
        });

        app.Run();
    }
}
