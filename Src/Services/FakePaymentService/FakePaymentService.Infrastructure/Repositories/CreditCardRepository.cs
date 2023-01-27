using FakePaymentService.Application.Interfaces.Repositories;
using FakePaymentService.Domain.Models;
using FakePaymentService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace FakePaymentService.Infrastructure.Repositories;

public class CreditCardRepository:GenericRepository<CreditCard>,ICreditCardRepository
{
    public CreditCardRepository(FakePaymentDbContex db) : base(db)
    {
    }

    public async Task<CreditCard> GetByCardNumber(string cardNumber)
    {
        var creditCard =await  Table.FirstOrDefaultAsync(x => x.CardNumber == cardNumber);

        if (creditCard == null)
            throw new Exception("CreditCard Not Found");

        return creditCard;
    }
}