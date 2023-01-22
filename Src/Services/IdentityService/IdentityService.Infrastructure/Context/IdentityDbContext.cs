using IdentityService.Domain.Models;
using IdentityService.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Context;

public class IdentityDbContext:DbContext
{
    public IdentityDbContext(DbContextOptions options):base(options)
    {}


    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }
}