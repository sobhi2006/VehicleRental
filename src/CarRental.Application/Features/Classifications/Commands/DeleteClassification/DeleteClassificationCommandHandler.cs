using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Classifications.Commands.DeleteClassification;

/// <summary>
/// Handles deletion of Classification.
/// </summary>
public class DeleteClassificationCommandHandler : IRequestHandler<DeleteClassificationCommand, Result>
{
    private readonly IClassificationService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteClassificationCommandHandler"/> class.
    /// </summary>
    public DeleteClassificationCommandHandler(IClassificationService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeleteClassificationCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
