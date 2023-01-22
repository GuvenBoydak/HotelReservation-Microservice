using Bank.Domain.Models;
using MediatR;

namespace Bank.Application.Features.Commands.Users.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }
}