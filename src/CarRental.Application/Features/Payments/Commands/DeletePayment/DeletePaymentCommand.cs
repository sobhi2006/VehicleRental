using MediatR;
using CarRental.Application.Common;

namespace CarRental.Application.Features.Payments.Commands.DeletePayment;

/// <summary>
/// Command to delete a Payment.
/// </summary>
/// <param name="Id">Identifier of the Payment.</param>
public record DeletePaymentCommand(long Id) : IRequest<Result>;
