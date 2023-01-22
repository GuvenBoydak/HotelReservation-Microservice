using IdentityService.Domain.Models;
using Shared.Infrastructure.EntityFramework;

namespace IdentityService.Application.Interfaces.Repositories;

public interface IUserRepository:IRepository<User>
{
     Task<User> GetByEmailAsync(string email);
}