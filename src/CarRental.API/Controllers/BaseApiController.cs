using Asp.Versioning;
using CarRental.API.Authorization;
using CarRental.Application.Common.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Policy = AuthorizationPolicies.ActiveUserPolicy, Roles = $"{ApplicationRoles.Owner},{ApplicationRoles.Admin},{ApplicationRoles.Labor}")]
[Produces("application/json")]
/// <summary>
/// Base controller providing mediator access.
/// </summary>
public abstract class BaseApiController : ControllerBase
{
    private ISender? _mediator;

    /// <summary>
    /// Gets the MediatR sender from the request services.
    /// </summary>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
