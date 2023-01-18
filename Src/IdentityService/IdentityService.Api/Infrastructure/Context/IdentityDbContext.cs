using Microsoft.EntityFrameworkCore;

namespace IdentityService.Api.Infrastructure.Context;

public class IdentityDbContext:DbContext
{
    public IdentityDbContext(DbContextOptions options):base(options)
    {
        
    }
}