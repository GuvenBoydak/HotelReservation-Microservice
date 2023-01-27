using AutoMapper;
using FluentValidation.AspNetCore;
using HotelReservationService.Application.Features.Commands.Package.CreatePackage;
using HotelReservationService.Application.Mapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shared.Filters;

namespace HotelReservationService.Application.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection service)
    {

        service.AddMediatR(typeof(ServiceRegistration));
        
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapProfile());
        });
        service.AddSingleton(mapperConfig.CreateMapper());
        
       service.AddControllers(option => option.Filters.Add<ValidatorFilterAttribute>()).AddFluentValidation(x => 
            x.RegisterValidatorsFromAssemblyContaining(typeof(CreatePackageCommand)));
        
        return service;
    }
}