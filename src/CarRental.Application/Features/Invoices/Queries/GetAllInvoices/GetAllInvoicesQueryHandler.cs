using AutoMapper;
using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Invoice;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Invoices.Queries.GetAllInvoices;

/// <summary>
/// Handles retrieving all Invoices.
/// </summary>
public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, Result<PaginatedList<InvoiceDto>>>
{
    private readonly IInvoiceService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllInvoicesQueryHandler"/> class.
    /// </summary>
    public GetAllInvoicesQueryHandler(IInvoiceService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<InvoiceDto>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
    {
        var serviceResult = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        if (serviceResult.IsFailure || serviceResult.Value is null)
        {
            return serviceResult.Errors.Count > 0
                ? Result<PaginatedList<InvoiceDto>>.Failure(serviceResult.Errors)
                : Result<PaginatedList<InvoiceDto>>.Failure(serviceResult.Error ?? "Failed to retrieve Invoices.");
        }

        var paginated = new PaginatedList<InvoiceDto>(
            _mapper.Map<List<InvoiceDto>>(serviceResult.Value.Items),
            serviceResult.Value.TotalCount,
            serviceResult.Value.PageNumber,
            serviceResult.Value.PageSize);

        return Result<PaginatedList<InvoiceDto>>.Success(paginated);
    }
}
