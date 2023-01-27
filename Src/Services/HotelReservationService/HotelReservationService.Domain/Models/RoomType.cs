using Shared.Models;

namespace HotelReservationService.Domain.Models;

public class RoomType:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDoubleRoom { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    //Relational Property
    public List<Reservation> Reservations { get; set; }

}