using IdentityService.Api;
using IdentityService.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.Helpers;

namespace UnitTest.IdentityServerTest.ControllerTest;

public class BaseControllerTest : IClassFixture<TestApiServer<IdentityService.Api.Program, IdentityDbContext>>
{
    public readonly TestApiServer<IdentityService.Api.Program, IdentityDbContext> _factory;
    public readonly IdentityDbContext _db;

    public BaseControllerTest(TestApiServer<Program, IdentityDbContext> factory)
    {
        _factory = factory;
        var scope = _factory.Services.CreateScope();
        _db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
        _db.Database.EnsureCreated();
    }
}