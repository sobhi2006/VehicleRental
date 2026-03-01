using MediatR;
using AutoMapper;
using CarRental.Application.Common;
using CarRental.Application.DTOs.FeesBank;
using CarRental.Application.Interfaces;
using CarRental.Domain.Entities;

namespace CarRental.Application.Features.FeesBanks.Commands.CreateFeesBank;

/// <summary>
/// Handles creation of FeesBank.
/// </summary>
public class CreateFeesBankCommandHandler : IRequestHandler<CreateFeesBankCommand, Result<FeesBankDto>>
{
    private readonly IFeesBankService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateFeesBankCommandHandler"/> class.
    /// </summary>
    public CreateFeesBankCommandHandler(IFeesBankService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<FeesBankDto>> Handle(CreateFeesBankCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<FeesBank>(request);

        var result = await _service.CreateAsync(entity, cancellationToken);
        return result.MapResult(value => _mapper.Map<FeesBankDto>(value));
    }
}
