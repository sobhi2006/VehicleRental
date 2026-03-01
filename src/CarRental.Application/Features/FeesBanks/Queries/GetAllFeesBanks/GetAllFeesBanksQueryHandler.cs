using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.FeesBanks.Queries.GetAllFeesBanks;

/// <summary>
/// Handles retrieving all FeesBanks.
/// </summary>
public class GetAllFeesBanksQueryHandler : IRequestHandler<GetAllFeesBanksQuery, Result<PaginatedList<FeesBankDto>>>
{
    private readonly IFeesBankService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllFeesBanksQueryHandler"/> class.
    /// </summary>
    public GetAllFeesBanksQueryHandler(IFeesBankService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<PaginatedList<FeesBankDto>>> Handle(GetAllFeesBanksQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(request.PageNumber, request.PageSize, cancellationToken);
        return result.MapPaginatedResult(value => _mapper.Map<FeesBankDto>(value));
    }
}
