using IdentityService.Application.Features.Queries.GetByIdUser;
using MediatR;

namespace IdentityService.Application.Features.Commands.Users.RegisterUser;

public class RegisterUserCommand:IRequest<UserDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}