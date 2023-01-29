using HotelReservationService.Application.IntegrationEvent.EventsHandler;
using MediatR;

namespace HotelReservationService.Application.Features.Commands.Reservation.DeleteExistingReservation;

public class DeleteExistingReservationCommand : IRequest
{
    public string ReservationConfirmationNumber { get; set; }
}