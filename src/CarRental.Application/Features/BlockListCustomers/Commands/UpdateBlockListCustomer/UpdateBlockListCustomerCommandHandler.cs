using AutoMapper;
using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.BlockListCustomer;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.BlockListCustomers.Commands.UpdateBlockListCustomer;

/// <summary>
/// Handles updates of BlockListCustomer.
/// </summary>
public class UpdateBlockListCustomerCommandHandler : IRequestHandler<UpdateBlockListCustomerCommand, Result<BlockListCustomerDto>>
{
    private readonly IBlockListCustomerService _service;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateBlockListCustomerCommandHandler"/> class.
    /// </summary>
    public UpdateBlockListCustomerCommandHandler(IBlockListCustomerService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result<BlockListCustomerDto>> Handle(UpdateBlockListCustomerCommand request, CancellationToken cancellationToken)
    {
        var getResult = await _service.GetByIdAsync(request.Id, cancellationToken);

        if (getResult.IsFailure || getResult.Value is null)
        {
            return getResult.Errors.Count > 0
                ? Result<BlockListCustomerDto>.Failure(getResult.Errors)
                : Result<BlockListCustomerDto>.Failure(getResult.Error ?? "BlockListCustomer not found.");
        }

        var entity = _mapper.Map(request, getResult.Value);

        var updateResult = await _service.UpdateAsync(entity, cancellationToken);

        if (updateResult.IsFailure || updateResult.Value is null)
        {
            return updateResult.Errors.Count > 0
                ? Result<BlockListCustomerDto>.Failure(updateResult.Errors)
                : Result<BlockListCustomerDto>.Failure(updateResult.Error ?? "Failed to update BlockListCustomer.");
        }

        return Result<BlockListCustomerDto>.Success(_mapper.Map<BlockListCustomerDto>(updateResult.Value));
    }
}
