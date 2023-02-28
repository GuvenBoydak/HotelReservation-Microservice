using IdentityService.Application.Features.Queries.GetByIdUser;
using MediatR;

namespace IdentityService.Application.Features.Commands.Users.UpdateUser;

public class UpdateUserCommand:IRequest<UserDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}