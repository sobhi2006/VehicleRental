using Microsoft.AspNetCore.Identity;

namespace CarRental.Infrastructure.Identity;

/// <summary>
/// Application user account used by ASP.NET Core Identity.
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
