using AutoMapper;
using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationService.Application.Features.Queries.Reservation.GetByConfirmationNumberReservation;

public class GetByConfirmationNumberReservationQueryHandler:IRequestHandler<GetByConfirmationNumberReservationQuery,ReservationDto>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    public GetByConfirmationNumberReservationQueryHandler(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }
    public async Task<ReservationDto> Handle(GetByConfirmationNumberReservationQuery request, CancellationToken cancellationToken)
    {
        var reservation = await _reservationRepository.Where(x => x.ConfirmationNumber == request.ConfirmationNumber)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (reservation == null)
            throw new Exception("Reservation Not Found");

        return _mapper.Map<Domain.Models.Reservation, ReservationDto>(reservation);
    }
}