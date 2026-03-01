namespace CarRental.Application.DTOs.Person;

public record UpdateNationalNoDto
{
    /// <summary>Gets or sets the identifier.</summary>
    public long Id { get; init; }
    /// <summary>Gets or sets the NationalNo.</summary>
    public string NationalNo { get; init; } = string.Empty;
}