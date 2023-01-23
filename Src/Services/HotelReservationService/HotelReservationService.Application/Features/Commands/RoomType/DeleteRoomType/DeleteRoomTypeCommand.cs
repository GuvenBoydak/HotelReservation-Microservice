using MediatR;

namespace HotelReservationService.Application.Features.Commands.RoomType.DeleteRoomType;

public class DeleteRoomTypeCommand:IRequest
{
    public Guid Id { get; set; }
}