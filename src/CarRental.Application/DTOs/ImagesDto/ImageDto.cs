namespace CarRental.Application.DTOs.ImagesDto;

public record ImageDto
{
    public long Id { get; init; }
    public string ImageUrl { get; init; } = string.Empty;
}