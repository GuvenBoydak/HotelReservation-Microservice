namespace FakePaymentService.Application.IntegrationEvent.Events;

public class FailedPaymentProcessIntegrationEvent:EventBus.Base.Events.IntegrationEvent
{
    public FailedPaymentProcessIntegrationEvent( string reservationEmail, string reservationConfirmationNumber)
    {
        ReservationEmail = reservationEmail;
        ReservationConfirmationNumber = reservationConfirmationNumber;
    }
    
    public string ReservationConfirmationNumber { get; }
    public string ReservationEmail { get; }
}