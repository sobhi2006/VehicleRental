using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.FeesBanks.Queries.GetFeesBankById;

/// <summary>
/// Handles retrieving a FeesBank by id.
/// </summary>
public class GetFeesBankByIdQueryHandler : IRequestHandler<GetFeesBankByIdQuery, Result<FeesBankDto>>
{
    private readonly IFeesBankService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetFeesBankByIdQueryHandler"/> class.
    /// </summary>
    public GetFeesBankByIdQueryHandler(IFeesBankService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the query.
    /// </summary>
    public async Task<Result<FeesBankDto>> Handle(GetFeesBankByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(request.Id, cancellationToken);
        return result.MapResult(value => _mapper.Map<FeesBankDto>(value));
    }
}
