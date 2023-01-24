using AutoMapper;
using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Reservation.GetAllReservation;

public class GetAllReservationQueryHandler : IRequestHandler<GetAllReservationQuery, List<ReservationListDto>>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetAllReservationQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    public async Task<List<ReservationListDto>> Handle(GetAllReservationQuery request, CancellationToken cancellationToken)
    {
        var reservations = await _reservationRepository.GetAllAsync();

        return _mapper.Map<List<Domain.Models.Reservation>, List<ReservationListDto>>(reservations);
    }
}