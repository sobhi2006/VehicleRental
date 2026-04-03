using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using CarRental.Application.Interfaces;
using CarRental.Application.Common.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Identity;

public sealed class UserManagementService : IUserManagementService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserManagementService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<Result<PaginatedList<UserDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageNumber <= 0)
        {
            return Result<PaginatedList<UserDto>>.Failure("PageNumber must be greater than 0.");
        }

        if (pageSize <= 0)
        {
            return Result<PaginatedList<UserDto>>.Failure("PageSize must be greater than 0.");
        }

        var query = _userManager.Users.OrderBy(x => x.Email);
        var totalCount = await query.CountAsync(cancellationToken);
        var users = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        var mapped = new List<UserDto>(users.Count);

        foreach (var user in users)
        {
            mapped.Add(await MapAsync(user));
        }

        var paginated = new PaginatedList<UserDto>(mapped, totalCount, pageNumber, pageSize);
        return Result<PaginatedList<UserDto>>.Success(paginated);
    }

    public Task<Result<UserDto>> GetCurrentAsync(string userId, CancellationToken cancellationToken)
    {
        return GetByIdAsync(userId, cancellationToken);
    }

    public async Task<Result<UserDto>> GetByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        return Result<UserDto>.Success(await MapAsync(user));
    }

    public async Task<Result<UserDto>> CreateAsync(CreateUserDto request, CancellationToken cancellationToken)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
        {
            return Result<UserDto>.Failure("A user with this email already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            IsActive = request.IsActive,
            EmailConfirmed = true
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            return Result<UserDto>.Failure(createResult.Errors.Select(x => x.Description).ToArray());
        }

        foreach (var role in request.Roles.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var normalizedRole = NormalizeRole(role);
            if (string.IsNullOrWhiteSpace(normalizedRole))
            {
                continue;
            }

            if (!await _roleManager.RoleExistsAsync(normalizedRole))
            {
                return Result<UserDto>.Failure($"Role '{normalizedRole}' does not exist.");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, normalizedRole);
            if (!addToRoleResult.Succeeded)
            {
                return Result<UserDto>.Failure(addToRoleResult.Errors.Select(x => x.Description).ToArray());
            }
        }

        return Result<UserDto>.Success(await MapAsync(user));
    }

    public async Task<Result<UserDto>> UpdateAsync(string userId, UpdateUserDto request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        var emailConflict = await _userManager.Users.AnyAsync(x => x.Id != userId && x.Email == request.Email, cancellationToken);
        if (emailConflict)
        {
            return Result<UserDto>.Failure("A user with this email already exists.");
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.UserName = request.Email;
        user.IsActive = request.IsActive;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result<UserDto>.Failure(updateResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result<UserDto>.Success(await MapAsync(user));
    }

    public async Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
        {
            return Result.Failure("User not found.");
        }

        var changeResult = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
        if (!changeResult.Succeeded)
        {
            return Result.Failure(changeResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result.Success();
    }

    public async Task<Result> ResetPasswordAsync(string userId, string newPassword, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
        {
            return Result.Failure("User not found.");
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
        if (!resetResult.Succeeded)
        {
            return Result.Failure(resetResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
        {
            return Result.Failure("User not found.");
        }

        var deleteResult = await _userManager.DeleteAsync(user);
        if (!deleteResult.Succeeded)
        {
            return Result.Failure(deleteResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result.Success();
    }

    public async Task<Result<UserDto>> SetActiveAsync(string userId, bool isActive, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        user.IsActive = isActive;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return Result<UserDto>.Failure(updateResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result<UserDto>.Success(await MapAsync(user));
    }

    public async Task<Result<UserDto>> AssignRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        var normalizedRole = NormalizeRole(roleName);
        if (string.IsNullOrWhiteSpace(normalizedRole))
        {
            return Result<UserDto>.Failure("Role name is required.");
        }

        if (!await _roleManager.RoleExistsAsync(normalizedRole))
        {
            return Result<UserDto>.Failure($"Role '{normalizedRole}' does not exist.");
        }

        if (await _userManager.IsInRoleAsync(user, normalizedRole))
        {
            return Result<UserDto>.Failure($"User already has role '{normalizedRole}'.");
        }

        var addResult = await _userManager.AddToRoleAsync(user, normalizedRole);
        if (!addResult.Succeeded)
        {
            return Result<UserDto>.Failure(addResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result<UserDto>.Success(await MapAsync(user));
    }

    public async Task<Result<UserDto>> UnassignRoleAsync(string userId, string roleName, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        var normalizedRole = NormalizeRole(roleName);
        if (string.IsNullOrWhiteSpace(normalizedRole))
        {
            return Result<UserDto>.Failure("Role name is required.");
        }

        if (!await _roleManager.RoleExistsAsync(normalizedRole))
        {
            return Result<UserDto>.Failure($"Role '{normalizedRole}' does not exist.");
        }

        if (!await _userManager.IsInRoleAsync(user, normalizedRole))
        {
            return Result<UserDto>.Failure($"User does not have role '{normalizedRole}'.");
        }

        var removeResult = await _userManager.RemoveFromRoleAsync(user, normalizedRole);
        if (!removeResult.Succeeded)
        {
            return Result<UserDto>.Failure(removeResult.Errors.Select(x => x.Description).ToArray());
        }

        return Result<UserDto>.Success(await MapAsync(user));
    }

    public async Task<Result<IReadOnlyCollection<string>>> GetAvailableRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles
            .OrderBy(x => x.Name)
            .Select(x => x.Name!)
            .ToListAsync(cancellationToken);

        return Result<IReadOnlyCollection<string>>.Success(roles);
    }

    private async Task<UserDto> MapAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email ?? string.Empty,
            IsActive = user.IsActive,
            Roles = roles.ToList().AsReadOnly()
        };
    }

    private static string NormalizeRole(string role)
    {
        if (string.Equals(role, ApplicationRoles.Owner, StringComparison.OrdinalIgnoreCase))
        {
            return ApplicationRoles.Owner;
        }

        if (string.Equals(role, ApplicationRoles.Admin, StringComparison.OrdinalIgnoreCase))
        {
            return ApplicationRoles.Admin;
        }

        if (string.Equals(role, ApplicationRoles.Labor, StringComparison.OrdinalIgnoreCase))
        {
            return ApplicationRoles.Labor;
        }

        return role.Trim();
    }
}
