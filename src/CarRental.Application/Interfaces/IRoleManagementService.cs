using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;

namespace CarRental.Application.Interfaces;

public interface IRoleManagementService
{
    Task<Result<IReadOnlyCollection<RoleDto>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<RoleDto>> GetByIdAsync(string roleId, CancellationToken cancellationToken);
    Task<Result<RoleDto>> CreateAsync(string roleName, CancellationToken cancellationToken);
    Task<Result<RoleDto>> UpdateAsync(string roleId, string roleName, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(string roleId, CancellationToken cancellationToken);
}
