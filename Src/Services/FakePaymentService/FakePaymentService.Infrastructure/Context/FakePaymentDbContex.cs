using FakePaymentService.Domain.Models;
using FakePaymentService.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FakePaymentService.Infrastructure.Context;

public class FakePaymentDbContex:DbContext
{
    public FakePaymentDbContex(DbContextOptions options):base(options)
    { }

    public DbSet<CreditCard> CreditCard { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CreditCardConfiguration());
    }
}