using AutoMapper;
using CarRental.Application.DTOs.Payment;
using CarRental.Application.Features.Payments.Commands.CreatePayment;
using CarRental.Application.Features.Payments.Commands.UpdatePayment;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Payments;

/// <summary>
/// AutoMapper profile for Payment mappings.
/// </summary>
public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<CreatePaymentCommand, Payment>();
        CreateMap<UpdatePaymentCommand, Payment>();
        CreateMap<Payment, PaymentDto>();
    }
}
