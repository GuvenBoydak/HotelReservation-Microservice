using AutoMapper;
using Bank.Application.Interfaces.Repositories;
using Bank.Application.Mapping;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Application.ServiceRegistration;

public static class ServisRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServisRegistration));
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapProfile());
        });
        services.AddSingleton(mapperConfig.CreateMapper());
    }
}