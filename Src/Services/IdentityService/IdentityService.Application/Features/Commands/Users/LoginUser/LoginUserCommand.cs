using IdentityService.Application.Dtos;
using MediatR;

namespace IdentityService.Application.Features.Commands.Users.LoginUser;

public class LoginUserCommand : IRequest<AccessToken>
{
    public string Email { get; set; }

    public string Password { get; set; }
}