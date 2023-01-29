namespace HotelReservationService.Application.IntegrationEvent.Events;

public class FailedPaymentProcessIntegrationEvent:EventBus.Base.Events.IntegrationEvent
{
    public FailedPaymentProcessIntegrationEvent(string reservationConfirmationNumber)
    {
        ReservationConfirmationNumber = reservationConfirmationNumber;
    }

    public string ReservationConfirmationNumber { get; set; }
}