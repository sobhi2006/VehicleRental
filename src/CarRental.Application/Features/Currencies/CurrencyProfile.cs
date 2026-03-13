using AutoMapper;
using CarRental.Application.DTOs.Currency;
using CarRental.Application.Features.Currencies.Commands.CreateCurrency;
using CarRental.Application.Features.Currencies.Commands.UpdateCurrency;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Currencies;

/// <summary>
/// AutoMapper profile for Currency mappings.
/// </summary>
public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        CreateMap<CreateCurrencyCommand, Currency>();
        CreateMap<UpdateCurrencyCommand, Currency>();
        CreateMap<Currency, CurrencyDto>();
    }
}
