using FakePaymentService.Domain.Models;
using Shared.Infrastructure.EntityFramework;

namespace FakePaymentService.Application.Interfaces.Repositories;

public interface ICreditCardRepository:IRepository<CreditCard>
{
    Task<CreditCard> GetByCardNumber(string cardNumber);
}