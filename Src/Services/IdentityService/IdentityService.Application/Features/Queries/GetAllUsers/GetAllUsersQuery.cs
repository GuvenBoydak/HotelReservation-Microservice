using MediatR;

namespace IdentityService.Api.Applications.Features.Queries.GetAllUsers;

public class GetAllUsersQuery : IRequest<List<UserListDto>>
{
}