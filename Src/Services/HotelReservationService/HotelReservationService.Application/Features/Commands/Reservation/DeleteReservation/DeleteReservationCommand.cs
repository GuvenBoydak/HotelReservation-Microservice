using MediatR;

namespace HotelReservationService.Application.Features.Commands.Reservation.DeleteReservation;

public class DeleteReservationCommand:IRequest
{
    public Guid Id { get; set; }
}