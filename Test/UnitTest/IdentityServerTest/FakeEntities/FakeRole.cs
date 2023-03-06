using IdentityService.Domain.Models;
using IdentityService.Infrastructure.Context;

namespace UnitTest.IdentityServerTest.FakeEntities;

public class FakeRole
{
    public readonly Role Admin;
    public readonly Role User;

    public FakeRole()
    {
        Admin = new Role() { Id = Guid.NewGuid(), Name = "Admin", CreatedDate = DateTime.UtcNow,IsDeleted = false};
        User = new Role() { Id = Guid.NewGuid(), Name = "User", CreatedDate = DateTime.UtcNow ,IsDeleted = false};
    }
    public void Add(IdentityDbContext db)
    {
        var dbSet = db.Set<Role>();
        dbSet.AddRange(Admin, User);
    }
}