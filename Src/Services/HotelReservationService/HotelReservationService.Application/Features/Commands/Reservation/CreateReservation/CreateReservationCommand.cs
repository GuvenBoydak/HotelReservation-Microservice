using HotelReservationService.Application.DTOs.ReservationDto;
using MediatR;

namespace HotelReservationService.Application.Features.Commands.Reservation.CreateReservation;

public class CreateReservationCommand : IRequest<CreateReservationDtoResponce>
{
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public int Pax { get; set; }
    public string ReservationName { get; set; }
    public string ReservationEmail { get; set; }
    public string ReservationPhoneNumber { get; set; }
    public Guid RoomTypeId { get; set; }
    public Guid PackageId { get; set; }

    public string CardNumber { get; set; }
    public DateTime CardExpiry { get; set; }
    public string CardName { get; set; }
    public string CardCVV { get; set; }
    public decimal Amount { get; set; }
}