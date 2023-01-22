using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Domain.Models;
using IdentityService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.EntityFramework;

namespace IdentityService.Infrastructure.Repository;

public class UserRepository:GenericRepository<User>,IUserRepository
{
    public UserRepository(IdentityDbContext db) : base(db)
    {
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await Table.FirstOrDefaultAsync(x => x.Email == email);
    }
}