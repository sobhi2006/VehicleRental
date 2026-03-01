using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BookingVehicle;
using CarRental.Application.Features.BookingVehicles.Commands.CreateBookingVehicle;
using CarRental.Application.Features.BookingVehicles.Commands.UpdateBookingVehicle;
using CarRental.Application.Features.BookingVehicles.Commands.DeleteBookingVehicle;
using CarRental.Application.Features.BookingVehicles.Queries.GetBookingVehicleById;
using CarRental.Application.Features.BookingVehicles.Queries.GetAllBookingVehicles;

namespace CarRental.API.Controllers;

/// <summary>
/// API endpoints for managing BookingVehicles
/// </summary>
public class BookingVehiclesController : BaseApiController
{
    /// <summary>
    /// Get all BookingVehicles
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<BookingVehicleDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllBookingVehiclesQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get a BookingVehicle by ID
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(BookingVehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBookingVehicleByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new BookingVehicle
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BookingVehicleDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBookingVehicleCommand command, CancellationToken cancellationToken)
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
    /// Update an existing BookingVehicle
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(BookingVehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateBookingVehicleCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a BookingVehicle
    /// </summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteBookingVehicleCommand(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return NoContent();
    }
}
