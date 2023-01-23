using HotelReservationService.Application.DTOs.RoomTypeDto;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.RoomType.GetAllRoomType;

public class GetAllRoomTypeQuery:IRequest<List<RoomTypeListDto>>
{
    
}