using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.Features.Queries.Package.GetByIdPackageQuery;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Reservation.GetByIdReservation;

public class GetByIdReservationQuery:IRequest<ReservationDto>
{
    public Guid Id { get; set; }
}