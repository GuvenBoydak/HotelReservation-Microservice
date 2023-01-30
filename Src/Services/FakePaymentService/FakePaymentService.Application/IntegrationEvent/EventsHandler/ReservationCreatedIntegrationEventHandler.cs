using EventBus.Base.Abstraction;
using FakePaymentService.Application.Features.Queries.CheckCreditCard;
using FakePaymentService.Application.IntegrationEvent.Events;
using MediatR;

namespace FakePaymentService.Application.IntegrationEvent.EventsHandler;

public class ReservationCreatedIntegrationEventHandler : IIntegrationEventHandler<ReservationCreatedIntegrationEvent>
{

    private readonly IMediator _mediator;

    public ReservationCreatedIntegrationEventHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Handle(ReservationCreatedIntegrationEvent @event)
    {
        var checkCreditCard = new CheckCreditCardQuery()
        {
            ReservationConfirmationNumber = @event.ReservationConfirmationNumber,
            ReservationEmail = @event.ReservationEmail, ReservationName = @event.ReservationName,
            ArrivalDate = @event.ArrivalDate, DepartureDate = @event.DepartureDate, CardExpiry = @event.CardExpiry,
            CardName = @event.CardName, CardNumber = @event.CardNumber, CardCVV = @event.CardCVV, Amount = @event.Amount
        };

        await _mediator.Send(checkCreditCard);
    }
}