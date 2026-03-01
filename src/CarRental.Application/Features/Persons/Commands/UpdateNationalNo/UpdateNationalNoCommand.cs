using CarRental.Application.Common;
using CarRental.Application.DTOs.Person;
using MediatR;

namespace CarRental.Application.Features.Persons.Commands.UpdateNationalNo;

public class UpdateNationalNoCommand : IRequest<Result<UpdateNationalNoDto>>
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the NationalNo.</summary>
    public string NationalNo { get; init; } = string.Empty;
}