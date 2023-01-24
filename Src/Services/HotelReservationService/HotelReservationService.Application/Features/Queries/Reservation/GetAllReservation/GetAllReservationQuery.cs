using HotelReservationService.Application.DTOs.ReservationDto;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Reservation.GetAllReservation;

public class GetAllReservationQuery:IRequest<List<ReservationListDto>>
{
}