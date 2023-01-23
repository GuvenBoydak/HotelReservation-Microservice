using HotelReservationService.Application.DTOs.RoomTypeDto;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.RoomType.GetByIdRoomType;

public class GetByIdRoomTypeQuery:IRequest<RoomTypeDto>
{
    public Guid Id { get; set; }
}