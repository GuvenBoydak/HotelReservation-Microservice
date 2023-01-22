using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Bank.Application.Features.Queries.Users.GetByIdUser;

public class GetByIdUserQuery:IRequest<UserDto>
{
    public Guid Id { get; set; }
}