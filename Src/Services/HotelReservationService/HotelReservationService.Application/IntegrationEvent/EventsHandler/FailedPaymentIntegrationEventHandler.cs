using EventBus.Base.Abstraction;
using HotelReservationService.Application.IntegrationEvent.Events;
using HotelReservationService.Application.İnterfaces.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.IntegrationEvent.EventsHandler;

public class FailedPaymentIntegrationEventHandler:IIntegrationEventHandler<FailedPaymentIntegrationEvent>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<FailedPaymentIntegrationEvent> _logger;

    public FailedPaymentIntegrationEventHandler(ILogger<FailedPaymentIntegrationEvent> logger, IReservationRepository reservationRepository, IRoomTypeRepository roomTypeRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(FailedPaymentIntegrationEvent @event)
    {
        var reservation = _reservationRepository.Where(x=>x.ConfirmationNumber==@event.ReservationConfirmationNumber).FirstOrDefault();

        if (reservation == null)
            _logger.LogInformation("Reservation not found with reservation confirmation number");

        var roomType = await _roomTypeRepository.GetById(reservation.RoomTypeId);
        roomType.Quantity += 1;
        
        _roomTypeRepository.Update(roomType);
        await _reservationRepository.DeleteAsync(reservation.Id);
        
        _logger.LogInformation("{ReservationReservationConfirmationNumber} Confirmed reservation number has been deleted", reservation.ConfirmationNumber);

        await _unitOfWork.SaveChangesAsync();
    }
}