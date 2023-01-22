using HotelReservationService.Application.İnterfaces.Repositories;
using HotelReservationService.Domain.Models;
using HotelReservationService.Infrastracture.Context;

namespace HotelReservationService.Infrastracture.Repositories;

public class RoomTypeRepository:GenericRepository<RoomType>,IRoomTypeRepository
{
    public RoomTypeRepository(ReservationDbContext db) : base(db)
    {
    }
}