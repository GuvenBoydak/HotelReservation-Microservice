using System.Runtime.Intrinsics.X86;
using FakePaymentService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FakePaymentService.Infrastructure.EntityConfigurations;

public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
{
    public void Configure(EntityTypeBuilder<CreditCard> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasData(
            new CreditCard()
            {
                Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, CardName = "Test", CardNumber = "5555112548076309",
                CardExpiry = new DateTime(2026,05,01,01,01,01,DateTimeKind.Utc), CardCVV = "852", Amount = 100000, IsDeleted = false
            },
            new CreditCard()
            {
                Id = Guid.NewGuid(), CreatedDate = DateTime.UtcNow, CardName = "Deneme", CardNumber = "5444112548076404",
                CardExpiry = new DateTime(2026,05,01,01,01,01,DateTimeKind.Utc), CardCVV = "861", Amount = 100000, IsDeleted = false
            });
    }
}