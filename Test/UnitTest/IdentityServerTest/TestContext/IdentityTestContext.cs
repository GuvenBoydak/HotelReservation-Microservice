using IdentityService.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.IdentityServerTest.FakeEntities;

namespace UnitTest.IdentityServerTest.TestContext;

public class IdentityTestContext
{
    public FakeUser User { get; set; }
    public FakeRole Role { get; set; }

    public IdentityTestContext()
    {
        Role = new FakeRole();
        User = new FakeUser(this);
    }

    public void AddDbContext(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

        try
        {
            Role.Add(dbContext);
            User.Add(dbContext);
            dbContext.SaveChangesAsync().Wait();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}