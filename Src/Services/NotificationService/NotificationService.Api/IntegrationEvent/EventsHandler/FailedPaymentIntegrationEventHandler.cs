using EventBus.Base.Abstraction;
using NotificationService.Api.IntegrationEvent.Events;
using NotificationService.Api.Services;

namespace NotificationService.Api.IntegrationEvent.EventsHandler;

public class FailedPaymentProcessIntegrationEventHandler : IIntegrationEventHandler<FailedPaymentProcessIntegrationEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<FailedPaymentProcessIntegrationEventHandler> _logger;

    public FailedPaymentProcessIntegrationEventHandler(IEventBus eventBus, ILogger<FailedPaymentProcessIntegrationEventHandler> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    public async Task Handle(FailedPaymentProcessIntegrationEvent @event)
    {
        await MailService.SendFailedAsync(@event.ReservationEmail);

        _eventBus.Publish(
            new FailedPaymentProcessIntegrationEvent(@event.ReservationEmail, @event.ReservationConfirmationNumber));

        var failedPaymentEvent =
            new FailedPaymentProcessIntegrationEvent(@event.ReservationEmail, @event.ReservationConfirmationNumber);

        try
        {
            _eventBus.Publish(failedPaymentEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "ERROR Publishing integration event: {IntegrationEventId} from NotificationService",
                failedPaymentEvent.Id);
            throw;
        }
    }
}