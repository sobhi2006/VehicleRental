using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Payments.Commands.DeletePayment;

/// <summary>
/// Handles deletion of Payment.
/// </summary>
public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, Result>
{
    private readonly IPaymentService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePaymentCommandHandler"/> class.
    /// </summary>
    public DeletePaymentCommandHandler(IPaymentService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
