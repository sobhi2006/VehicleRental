using Microsoft.AspNetCore.Mvc;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Invoice;
using CarRental.Application.Features.Invoices.Commands.DeleteInvoice;
using CarRental.Application.Features.Invoices.Queries.GetInvoiceById;
using CarRental.Application.Features.Invoices.Queries.GetAllInvoices;

namespace CarRental.API.Controllers;

/// <summary>
/// API endpoints for managing Invoices
/// </summary>
public class InvoicesController : BaseApiController
{
    /// <summary>
    /// Get all Invoices
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<InvoiceDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default)
    {
        var result = await Mediator.Send(new GetAllInvoicesQuery(pageNumber, pageSize), cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Get a Invoice by ID
    /// </summary>
    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(InvoiceDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetInvoiceByIdQuery(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return Ok(result.Value);
    }

    /// <summary>
    /// Delete a Invoice
    /// </summary>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new DeleteInvoiceCommand(id), cancellationToken);
        
        if (result.IsFailure)
        {
            return NotFound(new { error = result.Error });
        }

        return NoContent();
    }
}
