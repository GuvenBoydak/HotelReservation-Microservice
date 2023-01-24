using AutoMapper;
using EventBus.Base.Abstraction;
using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.Helper;
using HotelReservationService.Application.IntegrationEvent;
using HotelReservationService.Application.IntegrationEvent.Events;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.Reservation.CreateReservation;

public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, CreateReservationDtoResponce>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomTypeRepository _roomtypeRepository;
    private readonly IPackageRepository _packageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public CreateReservationCommandHandler(IReservationRepository reservationRepository, IUnitOfWork unitOfWork,
        IMapper mapper, IRoomTypeRepository roomtypeRepository, IPackageRepository packageRepository,
        IEventBus eventBus)
    {
        _reservationRepository = reservationRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _roomtypeRepository = roomtypeRepository;
        _packageRepository = packageRepository;
        _eventBus = eventBus;
    }

    public async Task<CreateReservationDtoResponce> Handle(CreateReservationCommand request,
        CancellationToken cancellationToken)
    {
        var reservation = _mapper.Map<CreateReservationCommand, Domain.Models.Reservation>(request);

        var roomtype = await _roomtypeRepository.GetById(request.RoomTypeId);
        if (roomtype.Quantity <= 0)
            throw new Exception("No available room found");

        var package = await _packageRepository.GetById(request.PackageId);

        reservation.TotalAmount = reservation.Pax * package.Price;
        reservation.ConfirmationNumber = GenerateConfirmationNumber.Generate();

        await _reservationRepository.AddAsync(reservation);

        roomtype.Quantity -= 1;
        _roomtypeRepository.Update(roomtype);

        await _unitOfWork.SaveChangesAsync();

        var reservationCreateIntegrationEvent = new ReservationCreatedIntegrationEvent(
            reservation.ConfirmationNumber, request.CardNumber, request.CardExpiry, request.CardName,
            request.CardCVV, reservation.TotalAmount,request.ReservationEmail,request.ReservationName,request.ArrivalDate,request.DepartureDate);
        _eventBus.Publish(reservationCreateIntegrationEvent);

        return new CreateReservationDtoResponce()
        {
            ReservationEmail = reservation.ReservationEmail,
            ReservationName = reservation.ReservationName,
            ReservationConfirmationNumber = reservation.ConfirmationNumber,
            ArrivalDate = reservation.ArrivalDate,
            DepartureDate = reservation.DepartureDate
        };
    }
}