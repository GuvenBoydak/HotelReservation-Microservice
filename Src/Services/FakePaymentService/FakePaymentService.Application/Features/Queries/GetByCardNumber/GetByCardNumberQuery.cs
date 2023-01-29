using FakePaymentService.Application.DTOs;
using FakePaymentService.Application.Interfaces.Repositories;
using MediatR;

namespace FakePaymentService.Application.Features.Queries.GetByCardNumber;

public class GetByCardNumberQuery:IRequest<CreditCardResponce>
{
    public string CardNumber { get; set; }
}

public class GetByCardNumberQueryHandler:IRequestHandler<GetByCardNumberQuery,CreditCardResponce>
{
    private readonly ICreditCardRepository _creditCardRepository;


    public GetByCardNumberQueryHandler(ICreditCardRepository creditCardRepository)
    {
        _creditCardRepository = creditCardRepository;

    }

    public async Task<CreditCardResponce> Handle(GetByCardNumberQuery request, CancellationToken cancellationToken)
    {
        var creditCard = await _creditCardRepository.GetByCardNumber(request.CardNumber);

        if(creditCard == null)
             return new CreditCardResponce(){Error = "invalid credit card.",IsSuccess = false};
        
        return new CreditCardResponce(){IsSuccess = true};
    }
}