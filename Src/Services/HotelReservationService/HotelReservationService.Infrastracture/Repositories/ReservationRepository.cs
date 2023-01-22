using HotelReservationService.Application.İnterfaces.Repositories;
using HotelReservationService.Domain.Models;
using HotelReservationService.Infrastracture.Context;

namespace HotelReservationService.Infrastracture.Repositories;

public class ReservationRepository:GenericRepository<Reservation>,IReservationRepository
{
    public ReservationRepository(ReservationDbContext db) : base(db)
    {
    }
}