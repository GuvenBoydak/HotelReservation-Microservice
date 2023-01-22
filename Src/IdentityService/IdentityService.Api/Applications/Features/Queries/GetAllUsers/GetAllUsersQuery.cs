using MediatR;

namespace Bank.Application.Features.Queries.Users.GetAllUsers;

public class GetAllUsersQuery : IRequest<List<UserListDto>>
{
}