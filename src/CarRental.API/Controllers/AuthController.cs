using CarRental.Application.Features.Auth.Commands.Login;
using CarRental.Application.Features.Auth.Commands.Logout;
using CarRental.Application.Features.Auth.Commands.RefreshToken;
using CarRental.Application.DTOs.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers;

public sealed class AuthController : BaseApiController
{
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(new { error = result.Error, errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return Unauthorized(new { error = result.Error, errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new LogoutCommand(request.Token), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error, errors = result.Errors });
        }

        return NoContent();
    }
}
