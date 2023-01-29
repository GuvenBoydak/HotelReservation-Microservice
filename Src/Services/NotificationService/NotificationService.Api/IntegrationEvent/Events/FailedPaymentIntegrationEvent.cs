namespace NotificationService.Api.IntegrationEvent.Events;

public class FailedPaymentProcessIntegrationEvent : EventBus.Base.Events.IntegrationEvent
{
    public FailedPaymentProcessIntegrationEvent(string reservationEmail, string reservationConfirmationNumber)
    {
        ReservationEmail = reservationEmail;
        ReservationConfirmationNumber = reservationConfirmationNumber;
    }

    public string ReservationEmail { get; }
    public string ReservationConfirmationNumber { get; }
}