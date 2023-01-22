using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Domain.Models;
using IdentityService.Infrastructure.Context;
using Shared.Infrastructure.EntityFramework;

namespace IdentityService.Infrastructure.Repository;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(IdentityDbContext db) : base(db)
    {
    }
}