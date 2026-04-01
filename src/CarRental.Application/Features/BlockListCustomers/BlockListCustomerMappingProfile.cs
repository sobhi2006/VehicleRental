using AutoMapper;
using CarRental.Application.DTOs.BlockListCustomer;
using CarRental.Application.Features.BlockListCustomers.Commands.CreateBlockListCustomer;
using CarRental.Application.Features.BlockListCustomers.Commands.UpdateBlockListCustomer;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.BlockListCustomers;

/// <summary>
/// AutoMapper profile for BlockListCustomer mappings.
/// </summary>
public class BlockListCustomerMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BlockListCustomerMappingProfile"/> class.
    /// </summary>
    public BlockListCustomerMappingProfile()
    {
        CreateMap<BlockListCustomer, BlockListCustomerDto>();

        CreateMap<CreateBlockListCustomerCommand, BlockListCustomer>();

        CreateMap<UpdateBlockListCustomerCommand, BlockListCustomer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
    }
}
