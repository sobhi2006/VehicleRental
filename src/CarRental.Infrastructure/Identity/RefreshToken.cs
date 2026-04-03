namespace CarRental.Infrastructure.Identity;

public sealed class RefreshToken
{
    public long Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string RefreshTokenHash { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime? RevokedAtUtc { get; set; }
    public string? ReplacedByRefreshTokenHash { get; set; }
    public string? ReasonRevoked { get; set; }

    public ApplicationUser User { get; set; } = null!;
}
