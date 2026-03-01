using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Drivers.Commands.DeleteDriver;

/// <summary>
/// Handles deletion of Driver.
/// </summary>
public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, Result>
{
    private readonly IDriverService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteDriverCommandHandler"/> class.
    /// </summary>
    public DeleteDriverCommandHandler(IDriverService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
