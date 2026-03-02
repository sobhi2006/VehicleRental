using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
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
