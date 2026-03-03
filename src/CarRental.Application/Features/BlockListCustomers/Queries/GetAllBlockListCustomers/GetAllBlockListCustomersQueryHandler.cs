using AutoMapper;
using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BlockListCustomer;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.BlockListCustomers.Queries.GetAllBlockListCustomers;

/// <summary>
/// Handles retrieving all BlockListCustomers.
/// </summary>
public class GetAllBlockListCustomersQueryHandler : IRequestHandler<GetAllBlockListCustomersQuery, Result<PaginatedList<BlockListCustomerDto>>>
{
    private readonly IBlockListCustomerService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllBlockListCustomersQueryHandler"/> class.
    /// </summary>
    public GetAllBlockListCustomersQueryHandler(IBlockListCustomerService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<BlockListCustomerDto>>> Handle(GetAllBlockListCustomersQuery request, CancellationToken cancellationToken)
    {
        var serviceResult = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);

        if (serviceResult.IsFailure || serviceResult.Value is null)
        {
            return serviceResult.Errors.Count > 0
                ? Result<PaginatedList<BlockListCustomerDto>>.Failure(serviceResult.Errors)
                : Result<PaginatedList<BlockListCustomerDto>>.Failure(serviceResult.Error ?? "Failed to retrieve BlockListCustomers.");
        }

        var paginated = new PaginatedList<BlockListCustomerDto>(
            _mapper.Map<List<BlockListCustomerDto>>(serviceResult.Value.Items),
            serviceResult.Value.TotalCount,
            serviceResult.Value.PageNumber,
            serviceResult.Value.PageSize);

        return Result<PaginatedList<BlockListCustomerDto>>.Success(paginated);
    }
}
