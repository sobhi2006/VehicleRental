using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Makes.Commands.DeleteMake;

/// <summary>
/// Handles deletion of Make.
/// </summary>
public class DeleteMakeCommandHandler : IRequestHandler<DeleteMakeCommand, Result>
{
    private readonly IMakeService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteMakeCommandHandler"/> class.
    /// </summary>
    public DeleteMakeCommandHandler(IMakeService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteMakeCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
