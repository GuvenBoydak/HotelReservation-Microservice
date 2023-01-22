using HotelReservationService.Infrastracture.Context;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Infrastracture.UnitOfWork;

public class HoteReservationUnitOfWork:IUnitOfWork
{
    private readonly ReservationDbContext _db;

    public HoteReservationUnitOfWork(ReservationDbContext db)
    {
        _db = db;
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}