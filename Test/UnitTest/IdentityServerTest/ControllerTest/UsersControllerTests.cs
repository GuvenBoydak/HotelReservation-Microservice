using System.Net;
using System.Net.Http.Json;
using IdentityService.Application.Dtos;
using IdentityService.Application.Features.Commands.Users.LoginUser;
using IdentityService.Application.Features.Commands.Users.RegisterUser;
using IdentityService.Application.Features.Queries.GetAllUsers;
using IdentityService.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shared.ResponceDto;
using UnitTest.Helpers;
using UnitTest.IdentityServerTest.Dtos;
using UnitTest.IdentityServerTest.TestContext;

namespace UnitTest.IdentityServerTest.ControllerTest;

public class UsersControllerTests : BaseControllerTest
{
    private readonly TestApiServer<IdentityService.Api.Program, IdentityDbContext> _factory;
    private IdentityTestContext _db;

    public UsersControllerTests(TestApiServer<IdentityService.Api.Program, IdentityDbContext> factory) : base(factory)
    {
        _factory = factory;
        var scope = _factory.Services.CreateScope();
        _db = scope.ServiceProvider.GetRequiredService<IdentityTestContext>();
        _db.AddDbContext(_factory.Services);
    }

    [Fact]
    public async Task should_success_get_user_list()
    {
        // act
        var client = _factory.CreateClient();

        var response = await client.GetAsync("api/Users");

        var userList =
            JsonConvert.DeserializeObject<CustomResponseDto<List<UserListDto>>>(
                await response.Content.ReadAsStringAsync());

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(2, userList.Data.Count);
    }

    [Fact]
    public async Task should_success_register_user()
    {
        //arrange
        var user = new RegisterUserCommand()
        {
            FirstName = "Lorem",
            LastName = "Ipsun",
            Email = "Lorem@lorem.com",
            Password = "123456"
        };

        // act
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/Users/register", user);

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task should_success_login_user()
    {
        //arrange
        var user = new LoginUserCommand()
        {
            Email = "admin@admin",
            Password = "12345678"
        };

        // act
        var client = _factory.CreateClient();
        var response = await client.PostAsJsonAsync("/api/Users/login", user);

        TokenDto.Token=
            JsonConvert.DeserializeObject<CustomResponseDto<AccessToken>>(await response.Content.ReadAsStringAsync()).Data.Token;

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(TokenDto.Token);
    }
}