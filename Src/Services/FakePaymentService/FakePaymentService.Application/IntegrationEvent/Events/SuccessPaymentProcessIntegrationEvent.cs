namespace FakePaymentService.Application.IntegrationEvent.Events;

public class SuccessPaymentProcessIntegrationEvent:EventBus.Base.Events.IntegrationEvent
{
    public SuccessPaymentProcessIntegrationEvent(string reservationConfirmationNumber, string reservationEmail, DateTime arrivalDate, DateTime departureDate, decimal amount, string reservationName)
    {
        ReservationConfirmationNumber = reservationConfirmationNumber;
        ReservationEmail = reservationEmail;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
        Amount = amount;
        ReservationName = reservationName;
    }

    public DateTime ArrivalDate { get; }
    public DateTime DepartureDate { get; }
    public decimal Amount { get; }
    public string ReservationName { get; }
    public string ReservationConfirmationNumber { get; }
    public string ReservationEmail { get; }
    

}