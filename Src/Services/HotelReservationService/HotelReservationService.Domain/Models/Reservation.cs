using Shared.Models;

namespace HotelReservationService.Domain.Models;

public class Reservation:BaseEntity
{
    public string ConfirmationNumber { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public int Pax { get; set; }
    public decimal TotalAmount{ get; set; }
    public string ReservationName { get; set; }
    public string ReservationEmail { get; set; }
    public string ReservationPhoneNumber { get; set; }
    public Guid RoomTypeId { get; set; }
    public Guid PackageId { get; set; }
    
    //Relational property
    public RoomType RoomType { get; set; }
    public Package Package { get; set; }
}