using MediatR;

namespace IdentityService.Application.Features.Queries.GetByIdUser;

public class GetByIdUserQuery:IRequest<UserDto>
{
    public Guid Id { get; set; }
}