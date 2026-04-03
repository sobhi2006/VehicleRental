using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CarRental.Application.Common.Authorization;

namespace CarRental.API.Authorization;

/// <summary>
/// Evaluates whether the current principal carries an active-user claim.
/// </summary>
public sealed class ActiveUserRequirementHandler : AuthorizationHandler<ActiveUserRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ActiveUserRequirement requirement)
    {
        var isActive = context.User.FindFirstValue(ApplicationClaims.IsActive);

        if (string.Equals(isActive, bool.TrueString, StringComparison.OrdinalIgnoreCase) ||
            string.Equals(isActive, "1", StringComparison.Ordinal))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
