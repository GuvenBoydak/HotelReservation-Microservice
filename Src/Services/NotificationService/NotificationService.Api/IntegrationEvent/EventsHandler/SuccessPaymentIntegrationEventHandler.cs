using EventBus.Base.Abstraction;
using NotificationService.Api.DTOs;
using NotificationService.Api.IntegrationEvent.Events;
using NotificationService.Api.Services;

namespace NotificationService.Api.IntegrationEvent.EventsHandler;

public class SuccessPaymentIntegrationEventHandler : IIntegrationEventHandler<SuccessPaymentIntegrationEvent>
{
    public async Task Handle(SuccessPaymentIntegrationEvent @event)
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