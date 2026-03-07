using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;

public class Image : BaseEntity
{
    public string Url { get; set; } = string.Empty;
}