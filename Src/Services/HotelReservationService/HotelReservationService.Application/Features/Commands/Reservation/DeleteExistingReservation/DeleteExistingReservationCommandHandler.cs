using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.Reservation.DeleteExistingReservation;

public class DeleteExistingReservationCommandHandler : AsyncRequestHandler<DeleteExistingReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteExistingReservationCommandHandler> _logger;

    public DeleteExistingReservationCommandHandler(ILogger<DeleteExistingReservationCommandHandler> logger,
        IReservationRepository reservationRepository, IRoomTypeRepository roomTypeRepository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _reservationRepository = reservationRepository;
        _roomTypeRepository = roomTypeRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(DeleteExistingReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = _reservationRepository
            .Where(x => x.ConfirmationNumber == request.ReservationConfirmationNumber).FirstOrDefault();

        if (reservation == null)
            _logger.LogInformation("Reservation not found with reservation confirmation number");

        var roomType = await _roomTypeRepository.GetById(reservation.RoomTypeId);
        roomType.Quantity += 1;

        _roomTypeRepository.Update(roomType);
        await _reservationRepository.DeleteAsync(reservation.Id);

        _logger.LogInformation(
            "{ReservationReservationConfirmationNumber} Confirmed reservation number has been deleted",
            reservation.ConfirmationNumber);

        await _unitOfWork.SaveChangesAsync();
    }
}