using AutoMapper;
using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Invoice;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Invoices.Commands.UpdateInvoice;

/// <summary>
/// Handles updates of Invoice.
/// </summary>
public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Result<InvoiceDto>>
{
    private readonly IInvoiceService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateInvoiceCommandHandler"/> class.
    /// </summary>
    public UpdateInvoiceCommandHandler(IInvoiceService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<InvoiceDto>> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var getResult = await _service.GetByIdAsync(request.Id, cancellationToken);

        if (getResult.IsFailure || getResult.Value is null)
        {
            return getResult.Errors.Count > 0
                ? Result<InvoiceDto>.Failure(getResult.Errors)
                : Result<InvoiceDto>.Failure(getResult.Error ?? "Invoice not found.");
        }

        var entity = _mapper.Map(request, getResult.Value);

        var updateResult = await _service.UpdateAsync(entity, cancellationToken);

        if (updateResult.IsFailure || updateResult.Value is null)
        {
            return updateResult.Errors.Count > 0
                ? Result<InvoiceDto>.Failure(updateResult.Errors)
                : Result<InvoiceDto>.Failure(updateResult.Error ?? "Failed to update Invoice.");
        }

        return Result<InvoiceDto>.Success(_mapper.Map<InvoiceDto>(updateResult.Value));
    }
}
