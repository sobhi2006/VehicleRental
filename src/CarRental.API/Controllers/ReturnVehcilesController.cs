using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Common;
using CarRental.Application.DTOs.ReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.CreateReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.UpdateReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Commands.DeleteReturnVehicle;
using CarRental.Application.Features.ReturnVehicles.Queries.GetReturnVehicleById;
using CarRental.Application.Features.ReturnVehicles.Queries.GetAllReturnVehicles;

namespace CarRental.API.Controllers;

/// <summary>
/// API endpoints for managing ReturnVehicles
/// </summary>
public class ReturnVehiclesController : BaseApiController
{
    /// <summary>
    /// Get all ReturnVehicles
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<ReturnVehicleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllReturnVehiclesQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get a ReturnVehicle by ID
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ReturnVehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetReturnVehicleByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new ReturnVehicle
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ReturnVehicleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateReturnVehicleCommand command, CancellationToken cancellationToken)
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
    /// Update an existing ReturnVehicle
    /// </summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(ReturnVehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateReturnVehicleCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a ReturnVehicle
    /// </summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteReturnVehicleCommand(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return NoContent();
    }
}
