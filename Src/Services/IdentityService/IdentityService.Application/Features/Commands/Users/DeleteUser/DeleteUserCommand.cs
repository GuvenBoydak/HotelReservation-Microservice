using MediatR;

namespace IdentityService.Application.Features.Commands.Users.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }
}