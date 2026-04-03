using FluentValidation;

namespace CarRental.Application.Features.Users.Commands.OwnerResetUserPassword;

public sealed class OwnerResetUserPasswordCommandValidator : AbstractValidator<OwnerResetUserPasswordCommand>
{
    public OwnerResetUserPasswordCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("NewPassword is required.")
            .MinimumLength(8).WithMessage("NewPassword must be at least 8 characters.");

        RuleFor(x => x.ConfirmNewPassword)
            .Equal(x => x.NewPassword).WithMessage("ConfirmNewPassword must match NewPassword.");
    }
}
