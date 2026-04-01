using AutoMapper;
using CarRental.Application.DTOs.Invoice;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.Invoices;

/// <summary>
/// AutoMapper profile for Invoice mappings.
/// </summary>
public class InvoiceMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceMappingProfile"/> class.
    /// </summary>
    public InvoiceMappingProfile()
    {
        CreateMap<Invoice, InvoiceDto>()
            .ForMember(dest => dest.InvoiceLines,
                opt => opt.MapFrom(src => src.InvoiceLines.Select(line => new InvoiceLineDto
                {
                    Description = line.Description,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                }).ToList()));
    }
}
