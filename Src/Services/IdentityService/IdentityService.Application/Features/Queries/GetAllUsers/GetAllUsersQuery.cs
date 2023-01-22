using MediatR;

namespace IdentityService.Application.Features.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<List<UserListDto>>
{
}