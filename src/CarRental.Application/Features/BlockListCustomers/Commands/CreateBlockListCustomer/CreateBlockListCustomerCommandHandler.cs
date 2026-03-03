using AutoMapper;
using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BlockListCustomer;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.BlockListCustomers.Commands.CreateBlockListCustomer;

/// <summary>
/// Handles creation of BlockListCustomer.
/// </summary>
public class CreateBlockListCustomerCommandHandler : IRequestHandler<CreateBlockListCustomerCommand, Result<BlockListCustomerDto>>
{
    private readonly IBlockListCustomerService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBlockListCustomerCommandHandler"/> class.
    /// </summary>
    public CreateBlockListCustomerCommandHandler(IBlockListCustomerService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<BlockListCustomerDto>> Handle(CreateBlockListCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<BlockListCustomer>(request);

        var serviceResult = await _service.CreateAsync(entity, cancellationToken);

        if (serviceResult.IsFailure || serviceResult.Value is null)
        {
            return serviceResult.Errors.Count > 0
                ? Result<BlockListCustomerDto>.Failure(serviceResult.Errors)
                : Result<BlockListCustomerDto>.Failure(serviceResult.Error ?? "Failed to create BlockListCustomer.");
        }

        return Result<BlockListCustomerDto>.Success(_mapper.Map<BlockListCustomerDto>(serviceResult.Value));
    }
}
