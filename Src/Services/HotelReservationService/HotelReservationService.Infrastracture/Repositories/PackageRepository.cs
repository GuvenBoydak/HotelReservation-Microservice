using HotelReservationService.Application.İnterfaces.Repositories;
using HotelReservationService.Domain.Models;
using HotelReservationService.Infrastracture.Context;

namespace HotelReservationService.Infrastracture.Repositories;

public class PackageRepository:GenericRepository<Package>,IPackageRepository
{
    public PackageRepository(ReservationDbContext db) : base(db)
    {
    }
}