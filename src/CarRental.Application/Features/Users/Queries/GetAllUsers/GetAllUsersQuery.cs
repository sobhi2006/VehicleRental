using CarRental.Application.Common;
using CarRental.Application.DTOs.Identity;
using MediatR;

namespace CarRental.Application.Features.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery(int PageNumber = 1, int PageSize = 10) : IRequest<Result<PaginatedList<UserDto>>>;
