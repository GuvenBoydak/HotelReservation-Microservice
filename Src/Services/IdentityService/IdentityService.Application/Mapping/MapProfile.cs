using AutoMapper;
using IdentityService.Application.Features.Commands.Users.UpdateUser;
using IdentityService.Application.Features.Queries.GetAllUsers;
using IdentityService.Application.Features.Queries.GetByIdUser;
using IdentityService.Domain.Models;

namespace IdentityService.Application.Mapping;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserListDto>().ReverseMap();
        CreateMap<User,UpdateUserCommand>().ReverseMap();
    }
}