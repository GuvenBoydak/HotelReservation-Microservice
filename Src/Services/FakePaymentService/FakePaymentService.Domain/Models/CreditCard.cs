using Shared.Models;

namespace FakePaymentService.Domain.Models;

public class CreditCard:BaseEntity
{
    public string CardNumber { get; set; }
    public DateTime CardExpiry { get; set; }
    public string CardName { get; set; }
    public string CardCVV { get; set; }
    public decimal Amount { get; set; }
}