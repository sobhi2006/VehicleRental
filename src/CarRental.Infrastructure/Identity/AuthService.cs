using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using CarRental.Infrastructure.Data;
using CarRental.Infrastructure.Identity.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CarRental.Infrastructure.Identity;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly ApplicationDbContext _dbContext;
    private readonly JwtOptions _jwtOptions;
    private readonly int _refreshTokenExpiryDays;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        JwtTokenGenerator jwtTokenGenerator,
        ApplicationDbContext dbContext,
        IConfiguration configuration,
        IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _dbContext = dbContext;
        _jwtOptions = jwtOptions.Value;
        _refreshTokenExpiryDays = configuration.GetValue<int?>("Jwt:RefreshTokenExpiryDays") ?? 14;
    }

    public async Task<Result<AuthResponseDto>> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user is null)
        {
            return Result<AuthResponseDto>.Failure("Invalid email or password.");
        }

        if (!user.IsActive)
        {
            return Result<AuthResponseDto>.Failure("This user is inactive.");
        }

        var loginResult = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!loginResult)
        {
            return Result<AuthResponseDto>.Failure("Invalid email or password.");
        }

        var issuedToken = await IssueRefreshTokenAsync(user, cancellationToken);
        var token = await _jwtTokenGenerator.GenerateAsync(user, issuedToken.PlainToken, issuedToken.ExpiresAtUtc);
        return Result<AuthResponseDto>.Success(token);
    }

    public async Task<Result<AuthResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken)
    {
        var tokenHash = HashToken(request.RefreshToken);
        var tokenEntity = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.RefreshTokenHash == tokenHash, cancellationToken);

        if (tokenEntity is null)
        {
            return Result<AuthResponseDto>.Failure("Invalid refresh token.");
        }

        if (tokenEntity.RevokedAtUtc is not null)
        {
            await RevokeAllActiveUserTokensAsync(tokenEntity.UserId, "Refresh token reuse detected.", cancellationToken);
            return Result<AuthResponseDto>.Failure("Refresh token has already been revoked.");
        }

        if (tokenEntity.ExpiresAtUtc <= DateTime.UtcNow)
        {
            tokenEntity.RevokedAtUtc = DateTime.UtcNow;
            tokenEntity.ReasonRevoked = "Expired refresh token.";
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result<AuthResponseDto>.Failure("Refresh token has expired.");
        }

        if (!tokenEntity.User.IsActive)
        {
            await RevokeAllActiveUserTokensAsync(tokenEntity.UserId, "User is inactive.", cancellationToken);
            return Result<AuthResponseDto>.Failure("This user is inactive.");
        }

        var rotatedToken = await IssueRefreshTokenAsync(tokenEntity.User, cancellationToken);
        tokenEntity.RevokedAtUtc = DateTime.UtcNow;
        tokenEntity.ReasonRevoked = "Rotated refresh token.";
        tokenEntity.ReplacedByRefreshTokenHash = HashToken(rotatedToken.PlainToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        var authResponse = await _jwtTokenGenerator.GenerateAsync(tokenEntity.User, rotatedToken.PlainToken, rotatedToken.ExpiresAtUtc);
        return Result<AuthResponseDto>.Success(authResponse);
    }

    public async Task<Result> LogoutAsync(LogoutRequestDto request, CancellationToken cancellationToken)
    {
        ClaimsPrincipal principal;
        try
        {
            principal = ValidateAccessToken(request.Token);
        }
        catch
        {
            return Result.Failure("Invalid token.");
        }

        var userId = principal.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var jti = principal.FindFirstValue(JwtRegisteredClaimNames.Jti);

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(jti))
        {
            return Result.Failure("Invalid token payload.");
        }

        var expiresAtUtc = ResolveTokenExpiryUtc(principal);

        var existingRevokedToken = await _dbContext.RevokedAccessTokens
            .AnyAsync(x => x.Jti == jti, cancellationToken);

        if (!existingRevokedToken)
        {
            _dbContext.RevokedAccessTokens.Add(new RevokedAccessToken
            {
                Jti = jti,
                UserId = userId,
                ExpiresAtUtc = expiresAtUtc,
                RevokedAtUtc = DateTime.UtcNow,
                Reason = "User logout."
            });
        }

        await RevokeAllActiveUserTokensAsync(userId, "User logout.", cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private ClaimsPrincipal ValidateAccessToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new SecurityTokenException("Token is required.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtOptions.Issuer,
            ValidAudience = _jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),
            ClockSkew = TimeSpan.Zero
        };

        return tokenHandler.ValidateToken(token, validationParameters, out _);
    }

    private static DateTime ResolveTokenExpiryUtc(ClaimsPrincipal principal)
    {
        var expClaim = principal.FindFirstValue(JwtRegisteredClaimNames.Exp);
        if (long.TryParse(expClaim, out var expUnix))
        {
            return DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
        }

        return DateTime.UtcNow;
    }

    private async Task<(string PlainToken, DateTime ExpiresAtUtc)> IssueRefreshTokenAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        var plainToken = GenerateSecureToken();
        var tokenEntity = new RefreshToken
        {
            UserId = user.Id,
            RefreshTokenHash = HashToken(plainToken),
            CreatedAtUtc = DateTime.UtcNow,
            ExpiresAtUtc = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays),
            ReasonRevoked = null
        };

        _dbContext.RefreshTokens.Add(tokenEntity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return (plainToken, tokenEntity.ExpiresAtUtc);
    }

    private async Task RevokeAllActiveUserTokensAsync(string userId, string reason, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var activeTokens = await _dbContext.RefreshTokens
            .Where(x => x.UserId == userId && x.RevokedAtUtc == null && x.ExpiresAtUtc > now)
            .ToListAsync(cancellationToken);

        foreach (var token in activeTokens)
        {
            token.RevokedAtUtc = now;
            token.ReasonRevoked = reason;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static string GenerateSecureToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    private static string HashToken(string token)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes);
    }
}
