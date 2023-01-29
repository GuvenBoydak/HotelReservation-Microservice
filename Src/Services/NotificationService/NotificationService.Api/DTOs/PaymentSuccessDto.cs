namespace NotificationService.Api.DTOs;

public class PaymentSuccessDto
{
    public string ConfirmationNumber { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public decimal TotalAmount{ get; set; }
    public string ReservationEmail { get; set; }
    public string ReservationName { get; set; }
}