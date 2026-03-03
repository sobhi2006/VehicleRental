using AutoMapper;
using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BlockListCustomer;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.BlockListCustomers.Queries.GetBlockListCustomerById;

/// <summary>
/// Handles retrieving a BlockListCustomer by id.
/// </summary>
public class GetBlockListCustomerByIdQueryHandler : IRequestHandler<GetBlockListCustomerByIdQuery, Result<BlockListCustomerDto>>
{
    private readonly IBlockListCustomerService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBlockListCustomerByIdQueryHandler"/> class.
    /// </summary>
    public GetBlockListCustomerByIdQueryHandler(IBlockListCustomerService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<BlockListCustomerDto>> Handle(GetBlockListCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var serviceResult = await _service.GetByIdAsync(request.Id, cancellationToken);

        if (serviceResult.IsFailure || serviceResult.Value is null)
        {
            return serviceResult.Errors.Count > 0
                ? Result<BlockListCustomerDto>.Failure(serviceResult.Errors)
                : Result<BlockListCustomerDto>.Failure(serviceResult.Error ?? "BlockListCustomer not found.");
        }

        return Result<BlockListCustomerDto>.Success(_mapper.Map<BlockListCustomerDto>(serviceResult.Value));
    }
}
