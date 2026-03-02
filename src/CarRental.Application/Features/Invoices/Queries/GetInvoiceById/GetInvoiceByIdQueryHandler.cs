using AutoMapper;
using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Invoice;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Invoices.Queries.GetInvoiceById;

/// <summary>
/// Handles retrieving a Invoice by id.
/// </summary>
public class GetInvoiceByIdQueryHandler : IRequestHandler<GetInvoiceByIdQuery, Result<InvoiceDto>>
{
    private readonly IInvoiceService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetInvoiceByIdQueryHandler"/> class.
    /// </summary>
    public GetInvoiceByIdQueryHandler(IInvoiceService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<InvoiceDto>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
    {
        var serviceResult = await _service.GetByIdAsync(request.Id, cancellationToken);

        if (serviceResult.IsFailure || serviceResult.Value is null)
        {
            return serviceResult.Errors.Count > 0
                ? Result<InvoiceDto>.Failure(serviceResult.Errors)
                : Result<InvoiceDto>.Failure(serviceResult.Error ?? "Invoice not found.");
        }

        return Result<InvoiceDto>.Success(_mapper.Map<InvoiceDto>(serviceResult.Value));
    }
}
