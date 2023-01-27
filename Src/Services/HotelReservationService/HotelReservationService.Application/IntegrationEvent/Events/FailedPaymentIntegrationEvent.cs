namespace HotelReservationService.Application.IntegrationEvent.Events;

public class FailedPaymentIntegrationEvent:EventBus.Base.Events.IntegrationEvent
{
    public FailedPaymentIntegrationEvent(string reservationConfirmationNumber)
    {
        ReservationConfirmationNumber = reservationConfirmationNumber;
    }

    public string ReservationConfirmationNumber { get; set; }
}