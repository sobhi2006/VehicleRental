using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Identity;

public sealed class RoleManagementService : IRoleManagementService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleManagementService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<IReadOnlyCollection<RoleDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles
            .OrderBy(x => x.Name)
            .Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name ?? string.Empty
            })
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyCollection<RoleDto>>.Success(roles);
    }

    public async Task<Result<RoleDto>> GetByIdAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
        if (role is null)
        {
            return Result<RoleDto>.Failure("Role not found.");
        }

        return Result<RoleDto>.Success(new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        });
    }

    public async Task<Result<RoleDto>> CreateAsync(string roleName, CancellationToken cancellationToken)
    {
        var normalizedName = roleName.Trim();
        if (await _roleManager.RoleExistsAsync(normalizedName))
        {
            return Result<RoleDto>.Failure($"Role '{normalizedName}' already exists.");
        }

        var role = new IdentityRole(normalizedName);
        var createResult = await _roleManager.CreateAsync(role);
        if (!createResult.Succeeded)
        {
            return Result<RoleDto>.Failure(createResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result<RoleDto>.Success(new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        });
    }

    public async Task<Result<RoleDto>> UpdateAsync(string roleId, string roleName, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
        if (role is null)
        {
            return Result<RoleDto>.Failure("Role not found.");
        }

        var normalizedName = roleName.Trim();
        var conflict = await _roleManager.Roles.AnyAsync(
            x => x.Id != roleId && x.Name != null && x.Name.ToLower() == normalizedName.ToLower(),
            cancellationToken);

        if (conflict)
        {
            return Result<RoleDto>.Failure($"Role '{normalizedName}' already exists.");
        }

        role.Name = normalizedName;
        role.NormalizedName = normalizedName.ToUpperInvariant();

        var updateResult = await _roleManager.UpdateAsync(role);
        if (!updateResult.Succeeded)
        {
            return Result<RoleDto>.Failure(updateResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result<RoleDto>.Success(new RoleDto
        {
            Id = role.Id,
            Name = role.Name ?? string.Empty
        });
    }

    public async Task<Result> DeleteAsync(string roleId, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);
        if (role is null)
        {
            return Result.Failure("Role not found.");
        }

        var deleteResult = await _roleManager.DeleteAsync(role);
        if (!deleteResult.Succeeded)
        {
            return Result.Failure(deleteResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result.Success();
    }
}
