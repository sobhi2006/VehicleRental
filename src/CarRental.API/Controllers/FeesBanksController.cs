using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;
using CarRental.Application.Features.FeesBanks.Commands.CreateFeesBank;
using CarRental.Application.Features.FeesBanks.Commands.UpdateFeesBank;
using CarRental.Application.Features.FeesBanks.Commands.DeleteFeesBank;
using CarRental.Application.Features.FeesBanks.Queries.GetFeesBankById;
using CarRental.Application.Features.FeesBanks.Queries.GetAllFeesBanks;

namespace CarRental.API.Controllers;

/// <summary>
/// API endpoints for managing FeesBanks
/// </summary>
public class FeesBanksController : BaseApiController
{
    /// <summary>
    /// Get all FeesBanks
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<FeesBankDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllFeesBanksQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get a FeesBank by ID
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(FeesBankDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetFeesBankByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new FeesBank
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(FeesBankDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateFeesBankCommand command, CancellationToken cancellationToken)
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
    /// Update an existing FeesBank
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(FeesBankDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateFeesBankCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a FeesBank
    /// </summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteFeesBankCommand(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return NoContent();
    }
}
