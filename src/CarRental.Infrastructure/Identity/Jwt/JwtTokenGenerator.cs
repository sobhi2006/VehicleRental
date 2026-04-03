using CarRental.Application.Common.Authorization;
using CarRental.Application.DTOs.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarRental.Infrastructure.Identity.Jwt;

public sealed class JwtTokenGenerator
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly UserManager<ApplicationUser> _userManager;

    public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions, UserManager<ApplicationUser> userManager)
    {
        _jwtOptions = jwtOptions;
        _userManager = userManager;
    }

    public async Task<AuthResponseDto> GenerateAsync(ApplicationUser user, string refreshToken, DateTime refreshTokenExpiresAtUtc)
    {
        var options = _jwtOptions.Value;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ApplicationClaims.IsActive, user.IsActive.ToString().ToLowerInvariant())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var expiresAtUtc = DateTime.UtcNow.AddMinutes(options.ExpiryMinutes);
        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: expiresAtUtc,
            signingCredentials: credentials);

        return new AuthResponseDto
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            ExpiresAtUtc = expiresAtUtc,
            RefreshTokenExpiresAtUtc = refreshTokenExpiresAtUtc,
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            Roles = roles.ToList().AsReadOnly(),
            IsActive = user.IsActive
        };
    }
}
