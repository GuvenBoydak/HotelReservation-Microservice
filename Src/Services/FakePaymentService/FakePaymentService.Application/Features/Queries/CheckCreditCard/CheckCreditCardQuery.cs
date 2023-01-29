using EventBus.Base.Abstraction;
using FakePaymentService.Application.IntegrationEvent.Events;
using FakePaymentService.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FakePaymentService.Application.Features.Queries.CheckCreditCard;

public class CheckCreditCardQuery : IRequest
{
    public string ReservationConfirmationNumber { get; set; }
    public string ReservationEmail { get; set; }
    public string ReservationName { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public string CardNumber { get; set; }
    public DateTime CardExpiry { get; set; }
    public string CardName { get; set; }
    public string CardCVV { get; set; }
    public decimal Amount { get; set; }
}

public class CheckCreditCardQueryHandler : AsyncRequestHandler<CheckCreditCardQuery>
{
    private readonly IEventBus _eventBus;
    private readonly IMediator _mediator;
    private readonly ILogger<CheckCreditCardQueryHandler> _logger;
    private readonly ICreditCardRepository _creditCardRepository;

    public CheckCreditCardQueryHandler(ILogger<CheckCreditCardQueryHandler> logger,
        IEventBus eventBus, IMediator mediator, ICreditCardRepository creditCardRepository)
    {
        _logger = logger;
        _eventBus = eventBus;
        _mediator = mediator;
        _creditCardRepository = creditCardRepository;
    }

    protected override async Task Handle(CheckCreditCardQuery request, CancellationToken cancellationToken)
    {
        var result =
            await CheckCreditCard(request.CardNumber, request.CardExpiry, request.CardCVV, request.Amount);

        if (!result)
        {
            var failedPaymentEvent =
                new FailedPaymentProcessIntegrationEvent(request.ReservationConfirmationNumber,
                    request.ReservationEmail);

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

        var successPaymentEvent = new SuccessPaymentIntegrationEvent(request.ReservationConfirmationNumber,
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
            _logger.LogInformation("The entered card was not found.");
            throw new Exception("The entered card was not found.");
        }

        if (creditCard.CardExpiry < cardExpiry)
        {
            _logger.LogInformation("credit card expired");
            throw new Exception("credit card expired");
        }

        if (creditCard.CardCVV != cardCVV)
        {
            _logger.LogInformation("The entered card was not found.");
            throw new Exception("The entered card was not found.");
        }

        if (creditCard.Amount < amount)
        {
            _logger.LogInformation("Insufficient balance");
            throw new Exception("Insufficient balance");
        }

        return true;
    }
}