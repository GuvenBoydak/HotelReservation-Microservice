using AutoMapper;
using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Reservation.GetByIdReservation;

public class GetByIdReservationQueryHandler : IRequestHandler<GetByIdReservationQuery, ReservationDto>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetByIdReservationQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<ReservationDto> Handle(GetByIdReservationQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.GetById(request.Id);
        if (reservation == null)
            throw new Exception("Reservation Not Found");

        return _mapper.Map<Domain.Models.Reservation,ReservationDto>(reservation);
    }
}