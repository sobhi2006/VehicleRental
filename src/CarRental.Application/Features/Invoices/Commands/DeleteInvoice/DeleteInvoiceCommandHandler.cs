using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Invoices.Commands.DeleteInvoice;

/// <summary>
/// Handles deletion of Invoice.
/// </summary>
public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, Result>
{
    private readonly IInvoiceService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteInvoiceCommandHandler"/> class.
    /// </summary>
    public DeleteInvoiceCommandHandler(IInvoiceService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
