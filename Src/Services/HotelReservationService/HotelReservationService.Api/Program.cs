using EventBus.Base.Abstraction;
using HotelReservationService.Application.IntegrationEvent.Events;
using HotelReservationService.Application.IntegrationEvent.EventsHandler;
using HotelReservationService.Application.ServiceRegistration;
using HotelReservationService.Infrastracture.ServiceRegistration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationService();

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
eventBus.Subscribe<FailedPaymentIntegrationEvent, FailedPaymentIntegrationEventHandler>();