using CarRental.Application.Common.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        var roles = new[]
        {
            ApplicationRoles.Owner,
            ApplicationRoles.Admin,
            ApplicationRoles.Labor
        };

        var existingRoleNames = await roleManager.Roles
            .Select(r => r.Name)
            .Where(name => name != null)
            .ToListAsync();

        var existingRoles = new HashSet<string>(existingRoleNames!, StringComparer.OrdinalIgnoreCase);

        foreach (var role in roles.Where(role => !existingRoles.Contains(role)))
        {
            var createRoleResult = await roleManager.CreateAsync(new IdentityRole(role));
            if (!createRoleResult.Succeeded)
            {
                var errors = string.Join(", ", createRoleResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to seed role '{role}': {errors}");
            }
        }

        var owners = await userManager.GetUsersInRoleAsync(ApplicationRoles.Owner);
        if (owners.Count > 0)
        {
            return;
        }

        const string ownerUserName = "owner";
        const string ownerEmail = "owner@carrental.local";
        const string ownerPassword = "Owner1234!";

        var existingOwnerUser = await userManager.Users.FirstOrDefaultAsync(
            x => x.UserName == ownerUserName || x.Email == ownerEmail);

        if (existingOwnerUser is null)
        {
            existingOwnerUser = new ApplicationUser
            {
                UserName = ownerUserName,
                Email = ownerEmail,
                FirstName = "System",
                LastName = "Owner",
                IsActive = true,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(existingOwnerUser, ownerPassword);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to seed owner user: {errors}");
            }
        }

        if (!await userManager.IsInRoleAsync(existingOwnerUser, ApplicationRoles.Owner))
        {
            var addToRoleResult = await userManager.AddToRoleAsync(existingOwnerUser, ApplicationRoles.Owner);
            if (!addToRoleResult.Succeeded)
            {
                var errors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to assign owner role to seeded user: {errors}");
            }
        }
    }
}
