using FakePaymentService.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.FakePaymentServiceTest.FakeEntities;

namespace UnitTest.FakePaymentServiceTest.TestSetup;

public class FakePaymentTestContext
{
    public FakeCreditCard TestCard { get; set; }

    public FakePaymentTestContext()
    {
        TestCard = new FakeCreditCard();
    }

    public void AddDbContext(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FakePaymentDbContex>();

        try
        {
            TestCard.Add(dbContext);
            dbContext.SaveChangesAsync().Wait();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}