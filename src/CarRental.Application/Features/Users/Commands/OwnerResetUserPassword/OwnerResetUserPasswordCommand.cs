using CarRental.Application.Common;
using MediatR;

namespace CarRental.Application.Features.Users.Commands.OwnerResetUserPassword;

public sealed record OwnerResetUserPasswordCommand(string UserId, string NewPassword, string ConfirmNewPassword) : IRequest<Result>;