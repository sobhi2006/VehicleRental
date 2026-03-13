using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Payments.Queries.GetAllPayments;

/// <summary>
/// Handles retrieving all Payments.
/// </summary>
public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentsQuery, Result<PaginatedList<PaymentDto>>>
{
    private readonly IPaymentService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllPaymentsQueryHandler"/> class.
    /// </summary>
    public GetAllPaymentsQueryHandler(IPaymentService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<PaymentDto>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<PaymentDto>(value));
    }
}
