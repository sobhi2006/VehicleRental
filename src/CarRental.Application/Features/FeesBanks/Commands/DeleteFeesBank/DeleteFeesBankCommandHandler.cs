using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.FeesBanks.Commands.DeleteFeesBank;

/// <summary>
/// Handles deletion of FeesBank.
/// </summary>
public class DeleteFeesBankCommandHandler : IRequestHandler<DeleteFeesBankCommand, Result>
{
    private readonly IFeesBankService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteFeesBankCommandHandler"/> class.
    /// </summary>
    public DeleteFeesBankCommandHandler(IFeesBankService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteFeesBankCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
