using MediatR;

namespace HotelReservationService.Application.Features.Commands.RoomType.UpdateRoomType;

public class UpdateRoomTypeCommand:IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsDoubleRoom { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}