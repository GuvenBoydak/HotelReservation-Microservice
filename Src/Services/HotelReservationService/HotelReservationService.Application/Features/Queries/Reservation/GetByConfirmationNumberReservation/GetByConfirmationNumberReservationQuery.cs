using HotelReservationService.Application.DTOs.ReservationDto;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Reservation.GetByConfirmationNumberReservation;

public class GetByConfirmationNumberReservationQuery:IRequest<ReservationDto>
{
    public string ConfirmationNumber { get; set; }
}