using MediatR;
using CarRental.Application.Common;
using CarRental.Application.Interfaces;

namespace CarRental.Application.Features.Persons.Commands.DeletePerson;

/// <summary>
/// Handles deletion of Person.
/// </summary>
public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Result>
{
    private readonly IPersonService _service;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeletePersonCommandHandler"/> class.
    /// </summary>
    public DeletePersonCommandHandler(IPersonService service)
    {
        _service = service;
    }

    /// <summary>
    /// Handles the command.
    /// </summary>
    public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        return await _service.DeleteAsync(request.Id, cancellationToken);
    }
}
