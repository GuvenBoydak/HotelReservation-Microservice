using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Service;
using IdentityService.Application.Service;
using IdentityService.Infrastructure.Context;
using IdentityService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.EntityFramework;

namespace IdentityService.Infrastructure.ServiceRegistration
{
    public static class ServiceRegisration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<IdentityDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("PostgreSql")));

            //Repository
            service.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IUnitOfWork, IdentityUnitOfWork>();
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IRoleRepository, RoleRepository>();

            //Service
            service.AddScoped<ITokenHelper, TokenHelper>();

            return service;
        }
    }
}
