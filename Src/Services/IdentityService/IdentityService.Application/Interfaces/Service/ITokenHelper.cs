using IdentityService.Application.Dtos;
using IdentityService.Domain.Models;

namespace IdentityService.Application.Interfaces.Service;

public interface ITokenHelper
{
    AccessToken CreateToken(User user);
}