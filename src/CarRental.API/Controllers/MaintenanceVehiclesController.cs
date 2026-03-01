using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Common;
using CarRental.Application.DTOs.MaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.CreateMaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.UpdateMaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Commands.DeleteMaintenanceVehicle;
using CarRental.Application.Features.MaintenanceVehicles.Queries.GetMaintenanceVehicleById;
using CarRental.Application.Features.MaintenanceVehicles.Queries.GetAllMaintenanceVehicles;

namespace CarRental.API.Controllers;

/// <summary>
/// API endpoints for managing MaintenanceVehicles
/// </summary>
public class MaintenanceVehiclesController : BaseApiController
{
    /// <summary>
    /// Get all MaintenanceVehicles
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<MaintenanceVehicleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllMaintenanceVehiclesQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get a MaintenanceVehicle by ID
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(MaintenanceVehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetMaintenanceVehicleByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new MaintenanceVehicle
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MaintenanceVehicleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceVehicleCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        var apiVersion = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";
        return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id, version = apiVersion }, result.Value);
    }

    /// <summary>
    /// Update an existing MaintenanceVehicle
    /// </summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(MaintenanceVehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateMaintenanceVehicleCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a MaintenanceVehicle
    /// </summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteMaintenanceVehicleCommand(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return NoContent();
    }
}
