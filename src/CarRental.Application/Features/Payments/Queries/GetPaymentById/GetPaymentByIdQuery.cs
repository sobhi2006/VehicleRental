using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Payment;

namespace CarRental.Application.Features.Payments.Queries.GetPaymentById;

/// <summary>
/// Query to get a Payment by id.
/// </summary>
/// <param name="Id">Identifier of the Payment.</param>
public record GetPaymentByIdQuery(long Id) : IRequest<Result<PaymentDto>>;
