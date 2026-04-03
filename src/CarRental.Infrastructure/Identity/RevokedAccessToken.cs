namespace CarRental.Infrastructure.Identity;

public sealed class RevokedAccessToken
{
    public long Id { get; set; }
    public string Jti { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime RevokedAtUtc { get; set; }
    public string? Reason { get; set; }
    public ApplicationUser? User { get; set; }
}
