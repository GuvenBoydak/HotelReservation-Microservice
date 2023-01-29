using HotelReservationService.Application.İnterfaces.Repositories;
using HotelReservationService.Infrastracture.Context;
using HotelReservationService.Infrastracture.Repositories;
using HotelReservationService.Infrastracture.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Infrastracture.ServiceRegistration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddDbContext<ReservationDbContext>(opt => opt.UseNpgsql(Configuration.ConnectionString));
        
        //Repository
        service.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        service.AddScoped<IUnitOfWork, HoteReservationUnitOfWork>();
        service.AddScoped<IReservationRepository, ReservationRepository>();
        service.AddScoped<IPackageRepository, PackageRepository>();
        service.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

        return service;
    }
}