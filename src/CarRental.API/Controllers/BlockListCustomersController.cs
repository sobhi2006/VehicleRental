using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BlockListCustomer;
using CarRental.Application.Features.BlockListCustomers.Commands.CreateBlockListCustomer;
using CarRental.Application.Features.BlockListCustomers.Commands.UpdateBlockListCustomer;
using CarRental.Application.Features.BlockListCustomers.Queries.GetBlockListCustomerById;
using CarRental.Application.Features.BlockListCustomers.Queries.GetAllBlockListCustomers;

namespace CarRental.API.Controllers;

/// <summary>
/// API endpoints for managing BlockListCustomers
/// </summary>
public class BlockListCustomersController : BaseApiController
{
    /// <summary>
    /// Get all BlockListCustomers
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<BlockListCustomerDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllBlockListCustomersQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get a BlockListCustomer by ID
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(BlockListCustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetBlockListCustomerByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new BlockListCustomer
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(BlockListCustomerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateBlockListCustomerCommand command, CancellationToken cancellationToken)
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
    /// Update an existing BlockListCustomer
    /// </summary>
    [HttpPut("{id:long}")]
    [ProducesResponseType(typeof(BlockListCustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateBlockListCustomerCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }
}
