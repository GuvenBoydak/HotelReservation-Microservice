using IdentityService.Api.Models;

namespace IdentityService.Api.Applications.Interfaces;

public interface ITokenHelper
{
    AccessToken CreateToken(User user);
}