using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;

namespace CarRental.Application.Interfaces;

public interface IUserManagementService
{
    Task<Result<PaginatedList<UserDto>>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
    Task<Result<UserDto>> GetCurrentAsync(string userId, CancellationToken cancellationToken);
    Task<Result<UserDto>> GetByIdAsync(string userId, CancellationToken cancellationToken);
    Task<Result<UserDto>> CreateAsync(CreateUserDto request, CancellationToken cancellationToken);
    Task<Result<UserDto>> UpdateAsync(string userId, UpdateUserDto request, CancellationToken cancellationToken);
    Task<Result> ChangePasswordAsync(string userId, string oldPassword, string newPassword, CancellationToken cancellationToken);
    Task<Result> ResetPasswordAsync(string userId, string newPassword, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(string userId, CancellationToken cancellationToken);
    Task<Result<UserDto>> SetActiveAsync(string userId, bool isActive, CancellationToken cancellationToken);
    Task<Result<UserDto>> AssignRoleAsync(string userId, string roleName, CancellationToken cancellationToken);
    Task<Result<UserDto>> UnassignRoleAsync(string userId, string roleName, CancellationToken cancellationToken);
    Task<Result<IReadOnlyCollection<string>>> GetAvailableRolesAsync(CancellationToken cancellationToken);
}
