using MediatR;

namespace FakePaymentService.Application.Features.Queries.CheckCreditCard;

public class CheckCreditCardQuery : IRequest
{
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