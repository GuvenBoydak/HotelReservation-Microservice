using FakePaymentService.Application.Interfaces.Repositories;
using FakePaymentService.Infrastructure.Context;
using FakePaymentService.Infrastructure.Repositories;
using HotelReservationService.Infrastracture;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EntityFramework;

namespace FakePaymentService.Infrastructure.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection service)
    {
        service.AddDbContext<FakePaymentDbContex>(opt => opt.UseNpgsql(Configuration.ConnectionString));
        
        //Repository
        service.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        service.AddScoped<ICreditCardRepository,CreditCardRepository>();

        return service;
    }
}