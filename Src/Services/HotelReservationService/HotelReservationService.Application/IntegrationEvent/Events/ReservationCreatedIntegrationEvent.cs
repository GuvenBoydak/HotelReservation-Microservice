namespace HotelReservationService.Application.IntegrationEvent.Events;

public class ReservationCreatedIntegrationEvent:EventBus.Base.Events.IntegrationEvent
{
    public ReservationCreatedIntegrationEvent(string reservationConfirmationNumber, string cardNumber, DateTime cardExpiry, string cardName, string cardCvv, decimal amount, string reservationEmail, string reservationName, DateTime arrivalDate, DateTime departureDate)
    {
        ReservationConfirmationNumber = reservationConfirmationNumber;
        CardNumber = cardNumber;
        CardExpiry = cardExpiry;
        CardName = cardName;
        CardCVV = cardCvv;
        Amount = amount;
        ReservationEmail = reservationEmail;
        ReservationName = reservationName;
        ArrivalDate = arrivalDate;
        DepartureDate = departureDate;
    }

    public string ReservationConfirmationNumber { get; set; }
    public string ReservationEmail { get; set; }
    public string ReservationName { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public string CardNumber { get; set; }
    public DateTime CardExpiry { get; set; }
    public string CardName { get; set; }
    public string CardCVV { get; set; }
    public decimal Amount { get; set; }
}