using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Payments.Commands.CreatePayment;

/// <summary>
/// Handles creation of Payment.
/// </summary>
public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Result<PaymentDto>>
{
    private readonly IPaymentService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreatePaymentCommandHandler"/> class.
    /// </summary>
    public CreatePaymentCommandHandler(IPaymentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<PaymentDto>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        return await _service.CreateAsync(request, cancellationToken);
    }
}
