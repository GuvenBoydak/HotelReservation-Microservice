namespace HotelReservationService.Application.DTOs.ReservationDto;

public class CreateReservationDtoResponce
{
    public string ReservationConfirmationNumber { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public string ReservationName { get; set; }
    public string ReservationEmail { get; set; }
}