using AutoMapper;
using IdentityService.Api.Applications.Features.Queries.GetAllUsers;
using IdentityService.Api.Applications.Features.Queries.GetByIdUser;
using IdentityService.Application.Features.Commands.Users.UpdateUser;
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