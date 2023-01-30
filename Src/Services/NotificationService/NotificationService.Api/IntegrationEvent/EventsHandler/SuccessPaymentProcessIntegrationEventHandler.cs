using EventBus.Base.Abstraction;
using NotificationService.Api.DTOs;
using NotificationService.Api.IntegrationEvent.Events;
using NotificationService.Api.Services;

namespace NotificationService.Api.IntegrationEvent.EventsHandler;

public class SuccessPaymentProcessIntegrationEventHandler : IIntegrationEventHandler<SuccessPaymentProcessIntegrationEvent>
{
    public async Task Handle(SuccessPaymentProcessIntegrationEvent @event)
    {
        var paymentSuccessDto = new PaymentSuccessDto()
        {
            ReservationEmail = @event.ReservationEmail,
            ConfirmationNumber = @event.ReservationConfirmationNumber,
            ArrivalDate = @event.ArrivalDate,
            DepartureDate = @event.DepartureDate,
            ReservationName = @event.ReservationName,
            TotalAmount = @event.Amount
        };
        
        await MailService.SendSuccessAsync(paymentSuccessDto);
    }
}