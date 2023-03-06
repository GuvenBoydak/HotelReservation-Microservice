using System.Text;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;
using FakePaymentService.Application.IntegrationEvent.Events;
using FakePaymentService.Application.IntegrationEvent.EventsHandler;
using FakePaymentService.Application.ServiceRegistration;
using FakePaymentService.Infrastructure.Context;
using FakePaymentService.Infrastructure.ServiceRegistration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FakePaymentService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseDefaultServiceProvider((context, options) =>
        {
            options.ValidateOnBuild = false;
            options.ValidateScopes = false;
        });

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("HotelReservationMicroserviceSuperSecretKey"));

        builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey
                };
            });


        builder.Services.AddTransient<ReservationCreatedIntegrationEventHandler>();

        builder.Services.AddApplicationService();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddSingleton<IEventBus>(sp =>
        {
            EventBusConfig config = new EventBusConfig()
            {
                ConnectionRetryCount = 5,
                EventNameSuffix = "IntegrationEvent",
                SubscriberClientAppName = "FakePaymentService"
            };
            return new EventBusRabbitMq(config, sp);
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
        eventBus.Subscribe<ReservationCreatedIntegrationEvent, ReservationCreatedIntegrationEventHandler>();

        app.Run();
    }
}