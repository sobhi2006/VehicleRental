using AutoMapper;
using CarRental.Application.DTOs.Invoice;
using CarRental.Application.Features.Invoices.Commands.CreateInvoice;
using CarRental.Application.Features.Invoices.Commands.UpdateInvoice;
using CarRental.Domain.Entities;

namespace CarRental.Application.Mappings;

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
                opt => opt.MapFrom(src => src.InvoiceLines.Select(line => new InvoiceLine
                {
                    Id = line.Id,
                    InvoiceId = line.InvoiceId,
                    Description = line.Description,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    LineTotal = line.LineTotal,
                    CreatedAt = line.CreatedAt,
                    UpdatedAt = line.UpdatedAt,
                    Invoice = null
                }).ToList()));

        CreateMap<CreateInvoiceCommand, Invoice>();

        CreateMap<UpdateInvoiceCommand, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
