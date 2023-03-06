using FakePaymentService.Application.Interfaces.Repositories;
using FakePaymentService.Infrastructure.Context;
using FakePaymentService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EntityFramework;

namespace FakePaymentService.Infrastructure.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddDbContext<FakePaymentDbContex>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("PostgreSql")));

        //Repository
        service.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        service.AddScoped<ICreditCardRepository, CreditCardRepository>();

        return service;
    }
}