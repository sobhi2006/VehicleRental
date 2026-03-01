using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;
using CarRental.Application.Features.Persons.Commands.CreatePerson;
using CarRental.Application.Features.Persons.Commands.UpdatePerson;
using CarRental.Application.Features.Persons.Commands.DeletePerson;
using CarRental.Application.Features.Persons.Queries.GetPersonById;
using CarRental.Application.Features.Persons.Queries.GetAllPersons;
using CarRental.Application.Features.Persons.Commands.UpdateNationalNo;

namespace CarRental.API.Controllers;

/// <summary>
/// API endpoints for managing Persons
/// </summary>
public class PersonsController : BaseApiController
{
    /// <summary>
    /// Get all Persons
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<PersonDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllPersonsQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get a Person by ID
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetPersonByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Create a new Person
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
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
    /// Update an existing Person
    /// </summary>
    [HttpPut]
    [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdatePersonCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a Person
    /// </summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeletePersonCommand(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return NoContent();
    }

    /// <summary>
    /// Update an existing Person
    /// </summary>
    [HttpPut("UpdateNationalNo")]
    [ProducesResponseType(typeof(UpdateNationalNoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateNationalNo([FromBody] UpdateNationalNoCommand command, CancellationToken cancellationToken)
    {
        Result<UpdateNationalNoDto>? result = await Mediator.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }
}
