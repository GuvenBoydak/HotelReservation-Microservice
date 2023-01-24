namespace HotelReservationService.Application.DTOs.ReservationDto;

public class ReservationListDto
{
    public Guid Id { get; set; }
    public string ReservationConfirmationNumber { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public int Pax { get; set; }
    public decimal TotalAmount{ get; set; }
    public string ReservationName { get; set; }
    public string ReservationEmail { get; set; }
    public string ReservationPhoneNumber { get; set; }
    public Guid RoomTypeId { get; set; }
    public Guid PackageId { get; set; }
}