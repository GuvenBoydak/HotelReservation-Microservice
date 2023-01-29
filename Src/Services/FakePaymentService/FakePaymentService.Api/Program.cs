using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;
using FakePaymentService.Application.IntegrationEvent.Events;
using FakePaymentService.Application.IntegrationEvent.EventsHandler;
using FakePaymentService.Application.ServiceRegistration;
using FakePaymentService.Infrastructure.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ReservationCreatedIntegrationEventHandler>();

builder.Services.AddApplicationService();
builder.Services.AddInfrastructureServices();

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

