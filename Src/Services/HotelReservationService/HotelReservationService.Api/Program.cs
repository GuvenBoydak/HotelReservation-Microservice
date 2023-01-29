using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;
using HotelReservationService.Api.Extensions;
using HotelReservationService.Application.IntegrationEvent.Events;
using HotelReservationService.Application.IntegrationEvent.EventsHandler;
using HotelReservationService.Application.İnterfaces.Repositories;
using HotelReservationService.Application.ServiceRegistration;
using HotelReservationService.Infrastracture.Repositories;
using HotelReservationService.Infrastracture.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<FailedPaymentProcessIntegrationEventHandler>();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationService();

builder.Services.ConfigureAuth(builder.Configuration);

builder.Services.AddSingleton<IEventBus>(sp =>
{
    EventBusConfig config = new EventBusConfig()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "HotelReservationService"
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
eventBus.Subscribe<FailedPaymentProcessIntegrationEvent, FailedPaymentProcessIntegrationEventHandler>();
app.Run();

