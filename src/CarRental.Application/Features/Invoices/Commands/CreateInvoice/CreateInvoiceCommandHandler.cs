using AutoMapper;
using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Invoice;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Invoices.Commands.CreateInvoice;

/// <summary>
/// Handles creation of Invoice.
/// </summary>
public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<InvoiceDto>>
{
    private readonly IInvoiceService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateInvoiceCommandHandler"/> class.
    /// </summary>
    public CreateInvoiceCommandHandler(IInvoiceService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<InvoiceDto>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Invoice>(request);

        var serviceResult = await _service.CreateAsync(entity, cancellationToken);

        if (serviceResult.IsFailure || serviceResult.Value is null)
        {
            return serviceResult.Errors.Count > 0
                ? Result<InvoiceDto>.Failure(serviceResult.Errors)
                : Result<InvoiceDto>.Failure(serviceResult.Error ?? "Failed to create Invoice.");
        }

        return Result<InvoiceDto>.Success(_mapper.Map<InvoiceDto>(serviceResult.Value));
    }
}
