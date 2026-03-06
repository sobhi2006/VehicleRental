using MediatR;
using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;

namespace CarRental.Application.Features.Persons.Commands.CreatePerson;

/// <summary>
/// Command to create a Person.
/// </summary>
public record CreatePersonCommand : IRequest<Result<PersonDto>>
{
    /// <summary>Gets or sets the FirstName.</summary>
    public string FirstName { get; init; } = string.Empty;
    /// <summary>Gets or sets the MiddleName.</summary>
    public string? MiddleName { get; init; } = string.Empty;
    /// <summary>Gets or sets the LastName.</summary>
    public string LastName { get; init; } = string.Empty;
    /// <summary>Gets or sets the NationalNo.</summary>
    public string NationalNo { get; init; } = string.Empty;
    /// <summary>Gets or sets the DateOfBirth.</summary>
    
    public DateOnly DateOfBirth { get; init; }
    /// <summary>Gets or sets the Email.</summary>
    public string Email { get; init; } = string.Empty;
    /// <summary>Gets or sets the PhoneNumber.</summary>
    public string PhoneNumber { get; init; } = string.Empty;
    /// <summary>Gets or sets the Address.</summary>
    public string Address { get; init; } = string.Empty;
}
