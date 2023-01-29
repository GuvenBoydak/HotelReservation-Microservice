using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.RabbitMQ;
using NotificationService.Api.IntegrationEvent.Events;
using NotificationService.Api.IntegrationEvent.EventsHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<SuccessPaymentIntegrationEventHandler>();
builder.Services.AddTransient<FailedPaymentProcessIntegrationEventHandler>();

builder.Services.AddSingleton<IEventBus>(sp =>
{
    EventBusConfig config = new EventBusConfig()
    {
        ConnectionRetryCount = 5,
        EventNameSuffix = "IntegrationEvent",
        SubscriberClientAppName = "NotificationService"
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

app.UseAuthorization();

app.MapControllers();

app.Run();

IEventBus eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<SuccessPaymentIntegrationEvent, SuccessPaymentIntegrationEventHandler>();
eventBus.Subscribe<FailedPaymentProcessIntegrationEvent, FailedPaymentProcessIntegrationEventHandler>();