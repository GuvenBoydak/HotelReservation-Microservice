using Shared.Models;

namespace HotelReservationService.Domain.Models;

public class Package:BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    //Relational Property
    public List<Reservation> Reservations { get; set; }
}