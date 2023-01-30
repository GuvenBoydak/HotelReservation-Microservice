using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FakePaymentService.Application.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection service)
    {
        //MediatR
        service.AddMediatR(typeof(ServiceRegistration));
        
        return service;
    }
}