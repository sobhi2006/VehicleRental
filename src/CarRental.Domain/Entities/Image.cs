using CarRental.Domain.Common;

namespace CarRental.Domain.Entities;

public abstract class Image : BaseEntity
{
    public string Url { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FakeName { get; set; } = string.Empty;
}