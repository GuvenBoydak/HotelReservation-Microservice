using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.RoomType.DeleteRoomType;

public class DeleteRoomTypeCommandHandler:AsyncRequestHandler<DeleteRoomTypeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoomTypeRepository _roomTypeRepository;

    public DeleteRoomTypeCommandHandler(IUnitOfWork unitOfWork, IRoomTypeRepository roomTypeRepository)
    {
        _unitOfWork = unitOfWork;
        _roomTypeRepository = roomTypeRepository;
    }

    protected override async Task Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
    {
        await _roomTypeRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}