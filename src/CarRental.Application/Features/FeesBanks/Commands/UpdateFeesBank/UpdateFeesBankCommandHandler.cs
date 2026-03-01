using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.FeesBanks.Commands.UpdateFeesBank;

/// <summary>
/// Handles updates of FeesBank.
/// </summary>
public class UpdateFeesBankCommandHandler : IRequestHandler<UpdateFeesBankCommand, Result<FeesBankDto>>
{
    private readonly IFeesBankService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateFeesBankCommandHandler"/> class.
    /// </summary>
    public UpdateFeesBankCommandHandler(IFeesBankService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<FeesBankDto>> Handle(UpdateFeesBankCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<FeesBank>(request);

        var result = await _service.UpdateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<FeesBankDto>(value));
    }
}
