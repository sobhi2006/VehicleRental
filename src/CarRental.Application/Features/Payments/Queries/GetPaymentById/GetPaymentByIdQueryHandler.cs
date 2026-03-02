using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Payments.Queries.GetPaymentById;

/// <summary>
/// Handles retrieving a Payment by id.
/// </summary>
public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Result<PaymentDto>>
{
    private readonly IPaymentService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetPaymentByIdQueryHandler"/> class.
    /// </summary>
    public GetPaymentByIdQueryHandler(IPaymentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaymentDto>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id, cancellationToken);
    }
}
