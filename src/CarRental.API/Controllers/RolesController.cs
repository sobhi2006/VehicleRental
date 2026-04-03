using CarRental.Application.Common.Authorization;
using CarRental.Application.Features.Roles.Commands.AssignUserRole;
using CarRental.Application.Features.Roles.Commands.CreateRole;
using CarRental.Application.Features.Roles.Commands.DeleteRole;
using CarRental.Application.Features.Roles.Commands.UnassignUserRole;
using CarRental.Application.Features.Roles.Commands.UpdateRole;
using CarRental.Application.Features.Roles.Queries.GetAllRoles;
using CarRental.Application.Features.Roles.Queries.GetRoleById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers;

[Authorize(Roles = $"{ApplicationRoles.Owner},{ApplicationRoles.Admin}")]
public sealed class RolesController : BaseApiController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetAllRolesQuery(), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpGet("{roleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(string roleId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetRoleByIdQuery(roleId), cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { errors = result.Errors });
        }

        var apiVersion = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
        return CreatedAtAction(nameof(GetById), new { roleId = result.Value!.Id, version = apiVersion }, result.Value);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateRoleCommand role, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UpdateRoleCommand(role.RoleId, role.Name), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error == "Role not found."
                ? NotFound(new { errors = result.Errors })
                : BadRequest(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpDelete("{roleId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(string roleId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteRoleCommand(roleId), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error == "Role not found."
                ? NotFound(new { errors = result.Errors })
                : BadRequest(new { errors = result.Errors });
        }

        return NoContent();
    }

    [HttpPost("{roleName}/users/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AssignUser(string roleName, string userId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new AssignUserRoleInRoleControllerCommand(userId, roleName), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error == "User not found."
                ? NotFound(new { errors = result.Errors })
                : BadRequest(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }

    [HttpDelete("{roleName}/users/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnassignUser(string roleName, string userId, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new UnassignUserRoleInRoleControllerCommand(userId, roleName), cancellationToken);

        if (result.IsFailure)
        {
            return result.Error == "User not found."
                ? NotFound(new { errors = result.Errors })
                : BadRequest(new { errors = result.Errors });
        }

        return Ok(result.Value);
    }
}
