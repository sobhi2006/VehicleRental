using CarRental.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.Infrastructure.Data.Configurations;

public sealed class RevokedAccessTokenConfiguration : IEntityTypeConfiguration<RevokedAccessToken>
{
    public void Configure(EntityTypeBuilder<RevokedAccessToken> builder)
    {
        builder.ToTable("RevokedAccessTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Jti)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ExpiresAtUtc)
            .IsRequired();

        builder.Property(x => x.RevokedAtUtc)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(300);

        builder.HasIndex(x => x.Jti)
            .IsUnique();

        builder.HasIndex(x => new { x.UserId, x.ExpiresAtUtc });
    }
}
