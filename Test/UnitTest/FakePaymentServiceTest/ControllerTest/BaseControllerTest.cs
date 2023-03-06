using System.Net.Http.Headers;
using FakePaymentService.Infrastructure.Context;
using IdentityService.Api;
using IdentityService.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.Helpers;
using UnitTest.IdentityServerTest.ControllerTest;
using UnitTest.IdentityServerTest.Dtos;

namespace UnitTest.FakePaymentServiceTest.ControllerTest;

public class BaseControllerTest : IClassFixture<TestApiServer<FakePaymentService.Api.Program, FakePaymentDbContex>>
{
    public readonly TestApiServer<FakePaymentService.Api.Program, FakePaymentDbContex> _factory;
    public readonly TestApiServer<IdentityService.Api.Program, IdentityDbContext> _identityFactory;
    public readonly FakePaymentDbContex _db;
    public HttpClient _client;

    public BaseControllerTest(TestApiServer<FakePaymentService.Api.Program, FakePaymentDbContex> factory)
    {
        _identityFactory = new TestApiServer<Program, IdentityDbContext>();
        var getToken = new UsersControllerTests(_identityFactory);
        getToken.should_success_login_user().GetAwaiter().GetResult();

        _factory = factory;
        var scope = _factory.Services.CreateScope();
        _db = scope.ServiceProvider.GetRequiredService<FakePaymentDbContex>();
        _db.Database.EnsureCreated();
        _client = _factory.CreateClient();

        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", $"{TokenDto.Token}");
    }
}