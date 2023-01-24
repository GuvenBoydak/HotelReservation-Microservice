using HotelReservationService.Domain.Models;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.İnterfaces.Repositories;

public interface IReservationRepository:IRepository<Reservation>
{
}