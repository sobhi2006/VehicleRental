using FluentValidation;

namespace CarRental.Application.Features.Roles.Commands.UnassignUserRole;

public sealed class UnassignUserRoleInRoleControllerCommandValidator : AbstractValidator<UnassignUserRoleInRoleControllerCommand>
{
    public UnassignUserRoleInRoleControllerCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("RoleName is required.");
    }
}
