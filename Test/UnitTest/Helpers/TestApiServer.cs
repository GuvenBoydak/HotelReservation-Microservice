using IdentityService.Api;
using IdentityService.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.FakePaymentServiceTest.TestSetup;
using UnitTest.IdentityServerTest.TestContext;

namespace UnitTest.Helpers;

public class TestApiServer<TProgram, TDbContext> : WebApplicationFactory<TProgram>, IAsyncLifetime
    where TProgram : class where TDbContext : DbContext
{
    public IConfiguration Config { get; }

    public TestApiServer()
    {
        Config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables()
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseConfiguration(Config);
        builder.ConfigureTestServices(service =>
        {
            service.BuildServiceProvider();

            service.AddScoped<IdentityTestContext>();
            service.AddScoped<FakePaymentTestContext>();
        });
    }

    public Task InitializeAsync()
    {
        DockerHelper.StartContainers();
        return Task.CompletedTask;
    }

    public Task DisposeAsync()
    {
        DockerHelper.StopContainers();
        return Task.CompletedTask;
    }
}

