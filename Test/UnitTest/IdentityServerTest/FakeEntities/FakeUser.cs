using IdentityService.Application.Helper;
using IdentityService.Domain.Models;
using IdentityService.Infrastructure.Context;
using UnitTest.IdentityServerTest.TestContext;

namespace UnitTest.IdentityServerTest.FakeEntities;

public class FakeUser
{
    public readonly User FirstTestUser;
    public readonly User SecoundTestUser;
    
    public FakeUser(IdentityTestContext db)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash("12345678", out passwordHash, out passwordSalt);

        FirstTestUser = new User()
        {
            Id = Guid.NewGuid(),
            Email = "admin@admin",
            FirstName = "First",
            LastName = "Test",
            CreatedDate = DateTime.UtcNow,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RoleId = db.Role.Admin.Id
        };
        SecoundTestUser = new User()
        {
            Id = Guid.NewGuid(),
            Email = "test@test",
            FirstName = "Secound",
            LastName = "Test",
            CreatedDate = DateTime.UtcNow,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RoleId = db.Role.User.Id
        };
    }

    public void Add(IdentityDbContext db)
    {
        var dbSet = db.Set<User>();
        dbSet.AddRange(FirstTestUser, SecoundTestUser);
    }
}