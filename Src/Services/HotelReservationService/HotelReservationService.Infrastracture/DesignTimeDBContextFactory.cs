using HotelReservationService.Infrastracture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HotelReservationService.Infrastracture;

internal class DesignTimeDBContextFactory : IDesignTimeDbContextFactory<ReservationDbContext>
{
    public ReservationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ReservationDbContext> dbContextOptionsBuilder = new();
        dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
        return new ReservationDbContext(dbContextOptionsBuilder.Options);

    }
}
