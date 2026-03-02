using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Payments.Commands.UpdatePayment;

/// <summary>
/// Handles updates of Payment.
/// </summary>
public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, Result<PaymentDto>>
{
    private readonly IPaymentService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdatePaymentCommandHandler"/> class.
    /// </summary>
    public UpdatePaymentCommandHandler(IPaymentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<PaymentDto>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        return await _service.UpdateAsync(request, cancellationToken);
    }
}
