using EventBus.Base.Abstraction;
using FakePaymentService.Application.IntegrationEvent.Events;
using FakePaymentService.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FakePaymentService.Application.Features.Queries.CheckCreditCard;

public class CheckCreditCardQueryHandler : AsyncRequestHandler<CheckCreditCardQuery>
{
    private readonly IEventBus _eventBus;
    private readonly ILogger<CheckCreditCardQueryHandler> _logger;
    private readonly ICreditCardRepository _creditCardRepository;

    public CheckCreditCardQueryHandler(ILogger<CheckCreditCardQueryHandler> logger,
        IEventBus eventBus, ICreditCardRepository creditCardRepository)
    {
        _logger = logger;
        _eventBus = eventBus;
        _creditCardRepository = creditCardRepository;
    }

    protected override async Task Handle(CheckCreditCardQuery request, CancellationToken cancellationToken)
    {
        var result =
            await CheckCreditCard(request.CardNumber, request.CardExpiry, request.CardCVV, request.Amount);

        if (!result)
        {
            var failedPaymentEvent =
                new FailedPaymentProcessIntegrationEvent(request.ReservationEmail,
                    request.ReservationConfirmationNumber);

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

        var successPaymentEvent = new SuccessPaymentProcessIntegrationEvent(request.ReservationConfirmationNumber,
            request.ReservationEmail, request.ArrivalDate, request.DepartureDate, request.Amount,
            request.ReservationName);
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


    private async Task<bool> CheckCreditCard(string cardNumber, DateTime cardExpiry, string cardCVV, decimal amount)
    {
        var creditCard = await _creditCardRepository.GetByCardNumber(cardNumber);

        if (creditCard is null)
        {
            _logger.LogInformation("CreditCard Not Found");
            throw new Exception("CreditCard Not Found");
        }

        if (creditCard.CardNumber != cardNumber)
        {
            _logger.LogInformation("The entered credit card wasn't found");
            return false;
        }

        if (creditCard.CardExpiry < cardExpiry)
        {
            _logger.LogInformation("credit card expired");
            return false;
        }

        if (creditCard.CardCVV != cardCVV)
        {
            _logger.LogInformation("The credit card cvv number entered doesn't match");
            return false;
        }

        if (creditCard.Amount < amount)
        {
            _logger.LogInformation("Insufficient balance");
            return false;
        }

        return true;
    }
}