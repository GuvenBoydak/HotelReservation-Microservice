using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.Reservation.DeleteReservation;

public class DeleteReservationCommandHandler:AsyncRequestHandler<DeleteReservationCommand>
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReservationCommandHandler(IUnitOfWork unitOfWork, IReservationRepository reservationRepository)
    {
        _unitOfWork = unitOfWork;
        _reservationRepository = reservationRepository;
    }

    protected override async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        await _reservationRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}