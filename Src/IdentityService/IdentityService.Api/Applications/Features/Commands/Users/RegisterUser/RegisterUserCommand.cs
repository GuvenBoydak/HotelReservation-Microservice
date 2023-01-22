using MediatR;

namespace Bank.Application.Features.Commands.Users.RegisterUser;

public class RegisterUserCommand:IRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
}