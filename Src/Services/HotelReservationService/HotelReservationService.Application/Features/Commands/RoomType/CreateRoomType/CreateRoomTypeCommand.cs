using MediatR;

namespace HotelReservationService.Application.Features.Commands.RoomType.CreateRoomType;

public class CreateRoomTypeCommand:IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDoubleRoom { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}