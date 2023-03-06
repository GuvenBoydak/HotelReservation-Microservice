using FakePaymentService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FakePaymentService.Infrastructure;

internal class DesignTimeDBContextFactory : IDesignTimeDbContextFactory<FakePaymentDbContex>
{
    public FakePaymentDbContex CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<FakePaymentDbContex> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
        return new FakePaymentDbContex(dbContextOptionsBuilder.Options);

    }
}
