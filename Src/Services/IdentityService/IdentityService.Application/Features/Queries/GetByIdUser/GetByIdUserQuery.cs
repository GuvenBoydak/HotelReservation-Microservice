using MediatR;

namespace IdentityService.Api.Applications.Features.Queries.GetByIdUser;

public class GetByIdUserQuery:IRequest<UserDto>
{
    public Guid Id { get; set; }
}