using EventBus.Base.Abstraction;
using HotelReservationService.Application.Features.Commands.Reservation.DeleteExistingReservation;
using HotelReservationService.Application.IntegrationEvent.Events;
using MediatR;

namespace HotelReservationService.Application.IntegrationEvent.EventsHandler;

public class
    FailedPaymentProcessIntegrationEventHandler : IIntegrationEventHandler<FailedPaymentProcessIntegrationEvent>
{
    private readonly IMediator _mediator;

    public FailedPaymentProcessIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(FailedPaymentProcessIntegrationEvent @event)
    {
        await _mediator.Send(new DeleteExistingReservationCommand
            { ReservationConfirmationNumber = @event.ReservationConfirmationNumber });
    }
}