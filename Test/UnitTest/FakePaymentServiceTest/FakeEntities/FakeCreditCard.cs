using FakePaymentService.Domain.Models;
using FakePaymentService.Infrastructure.Context;

namespace UnitTest.FakePaymentServiceTest.FakeEntities;

public class FakeCreditCard
{
    public readonly CreditCard TestCard;

    public FakeCreditCard()
    {
        TestCard = new CreditCard()
        {
            Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, CardName = "Test", CardNumber = "5555112548076309",
            CardExpiry = new DateTime(2026, 05, 01, 01, 01, 01, DateTimeKind.Utc), CardCVV = "852", Amount = 100000,
            IsDeleted = false
        };
    }

    public void Add(FakePaymentDbContex db)
    {
        var dbSet = db.Set<CreditCard>();
        dbSet.AddRange(TestCard);
    }
}