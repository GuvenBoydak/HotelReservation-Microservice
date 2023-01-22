using Bank.Application.DTOs.Token;
using MediatR;

namespace Bank.Application.Features.Commands.Users.LoginUser;

public class LoginUserCommand : IRequest<AccessToken>
{
    public string Email { get; set; }

    public string Password { get; set; }
}