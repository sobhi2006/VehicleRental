using CarRental.Application.Common.Authorization;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Features.Users.Commands.ChangePassword;
using CarRental.Application.Features.Users.Commands.CreateUser;
using CarRental.Application.Features.Users.Commands.DeleteUser;
using CarRental.Application.Features.Users.Commands.SetUserActive;
using CarRental.Application.Features.Users.Commands.UpdateUser;
using CarRental.Application.Features.Users.Queries.GetAllUsers;
using CarRental.Application.Features.Users.Queries.GetCurrentUser;
using CarRental.Application.Features.Users.Queries.GetUserById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarRental.API.Controllers;

public sealed class UsersController : BaseApiController
{
    [HttpGet]
    [Authorize(Roles = $"{ApplicationRoles.Owner},{ApplicationRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllUsersQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new { errors = new[] { "Unauthorized user." } });
        }

        var result = await Mediator.Send(new GetCurrentUserQuery(userId), cancellationToken);
        if (result.IsFailure)
        {
            return NotFound(new {errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpPut("me/password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangeMyPassword([FromBody] ChangePasswordDto request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized(new { errors = new[] { "Unauthorized user." } });
        }

        var result = await Mediator.Send(
            new ChangePasswordCommand(userId, request.OldPassword, request.NewPassword, request.ConfirmNewPassword),
            cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new {errors = result.Errors });
        }

        return NoContent();
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = $"{ApplicationRoles.Owner},{ApplicationRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string userId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserByIdQuery(userId), cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new {errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [Authorize(Roles = $"{ApplicationRoles.Owner},{ApplicationRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new {errors = result.Errors });
        }

        var apiVersion = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
        return CreatedAtAction(nameof(GetById), new { userId = result.Value!.Id, version = apiVersion }, result.Value);
    }

    [HttpPut]
    [Authorize(Roles = $"{ApplicationRoles.Owner},{ApplicationRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return result.Error == "User not found."
                ? NotFound(new {errors = result.Errors })
                : BadRequest(new {errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = $"{ApplicationRoles.Owner},{ApplicationRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string userId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteUserCommand(userId), cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new {errors = result.Errors });
        }

        return NoContent();
    }

    [HttpPatch("{userId}/active/{isActive:bool}")]
    [Authorize(Roles = $"{ApplicationRoles.Owner},{ApplicationRoles.Admin}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetActive(string userId, bool isActive, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new SetUserActiveCommand(userId, isActive), cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new {errors = result.Errors });
        }

        return Ok(result.Value);
    }

}
