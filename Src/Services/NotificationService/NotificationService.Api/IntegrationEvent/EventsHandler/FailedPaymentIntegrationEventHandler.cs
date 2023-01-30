using EventBus.Base.Abstraction;
using NotificationService.Api.IntegrationEvent.Events;
using NotificationService.Api.Services;

namespace NotificationService.Api.IntegrationEvent.EventsHandler;

public class FailedPaymentProcessIntegrationEventHandler : IIntegrationEventHandler<FailedPaymentProcessIntegrationEvent>
{
    public async Task Handle(FailedPaymentProcessIntegrationEvent @event)
    {
        await MailService.SendFailedAsync(@event.ReservationEmail);
    }
}