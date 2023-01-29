using EventBus.Base.Abstraction;
using FakePaymentService.Application.Features.Queries.CheckCreditCard;
using FakePaymentService.Application.IntegrationEvent.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FakePaymentService.Application.IntegrationEvent.EventsHandler;

public class ReservationCreatedIntegrationEventHandler : IIntegrationEventHandler<ReservationCreatedIntegrationEvent>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<ReservationCreatedIntegrationEventHandler> _logger;
    private readonly IMediator _mediator;

    public ReservationCreatedIntegrationEventHandler(ILogger<ReservationCreatedIntegrationEventHandler> logger,
        IEventBus eventBus, IMediator mediator)
    {
        _logger = logger;
        _eventBus = eventBus;
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
        

        var result =
            false; //await CheckCreditCard(@event.CardNumber, @event.CardExpiry, @event.CardCVV, @event.Amount);

        if (!result)
        {
            var failedPaymentEvent =
                new FailedPaymentProcessIntegrationEvent(@event.ReservationConfirmationNumber, @event.ReservationEmail);

            try
            {
                _eventBus.Publish(failedPaymentEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "ERROR Publishing integration event: {IntegrationEventId} from PaymentService",
                    failedPaymentEvent.Id);
                throw;
            }

            return;
        }

        var successPaymentEvent = new SuccessPaymentIntegrationEvent(@event.ReservationConfirmationNumber,
            @event.ReservationEmail, @event.ArrivalDate, @event.DepartureDate, @event.Amount, @event.ReservationName);
        try
        {
            _eventBus.Publish(successPaymentEvent);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "ERROR Publishing integration event: {IntegrationEventId} from PaymentService",
                successPaymentEvent.Id);
            throw;
        }
    }

    // private async Task<bool> CheckCreditCard(string cardNumber, DateTime cardExpiry, string cardCVV, decimal amount)
    // {
    //     var creditCard = await _creditCardRepository.GetByCardNumber(cardNumber);
    //
    //     if (creditCard is null)
    //     {
    //         _logger.LogInformation("CreditCard Not Found");
    //         throw new Exception("CreditCard Not Found");
    //     }
    //
    //     if (creditCard.CardNumber != cardNumber)
    //     {
    //         _logger.LogInformation("The entered card was not found.");
    //         throw new Exception("The entered card was not found.");
    //     }
    //
    //     if (creditCard.CardExpiry < cardExpiry)
    //     {
    //         _logger.LogInformation("credit card expired");
    //         throw new Exception("credit card expired");
    //     }
    //
    //     if (creditCard.CardCVV != cardCVV)
    //     {
    //         _logger.LogInformation("The entered card was not found.");
    //         throw new Exception("The entered card was not found.");
    //     }
    //
    //     if (creditCard.Amount < amount)
    //     {
    //         _logger.LogInformation("Insufficient balance");
    //         throw new Exception("Insufficient balance");
    //     }
    //
    //     return true;
    // }
}