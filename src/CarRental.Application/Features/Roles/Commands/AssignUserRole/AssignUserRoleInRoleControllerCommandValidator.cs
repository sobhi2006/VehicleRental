using FluentValidation;

namespace CarRental.Application.Features.Roles.Commands.AssignUserRole;

public sealed class AssignUserRoleInRoleControllerCommandValidator : AbstractValidator<AssignUserRoleInRoleControllerCommand>
{
    public AssignUserRoleInRoleControllerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("RoleName is required.");
    }
}
