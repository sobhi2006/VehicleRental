using Microsoft.AspNetCore.Authorization;

namespace CarRental.API.Authorization;

/// <summary>
/// Authorization requirement that allows only active users.
/// </summary>
public sealed class ActiveUserRequirement : IAuthorizationRequirement
{
}
