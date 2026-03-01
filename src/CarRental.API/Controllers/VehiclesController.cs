using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Vehicle;
using CarRental.Application.Features.Vehicles.Commands.CreateVehicle;
using CarRental.Application.Features.Vehicles.Commands.UpdateVehicle;
using CarRental.Application.Features.Vehicles.Commands.DeleteVehicle;
using CarRental.Application.Features.Vehicles.Queries.GetVehicleById;
using CarRental.Application.Features.Vehicles.Queries.GetAllVehicles;

namespace CarRental.API.Controllers;

/// <summary>
/// API endpoints for managing Vehicles
/// </summary>
public class VehiclesController : BaseApiController
{
    /// <summary>
    /// Get all Vehicles
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<VehicleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllVehiclesQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get a Vehicle by ID
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetVehicleByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new Vehicle
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateVehicleCommand command, CancellationToken cancellationToken)
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
    /// Update an existing Vehicle
    /// </summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateVehicleCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a Vehicle
    /// </summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteVehicleCommand(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return NoContent();
    }
}
