using CarRental.Application.Common;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.ChangePassword;

public sealed record ChangePasswordCommand(string UserId, string OldPassword, string NewPassword, string ConfirmNewPassword) : IRequest<Result>;
