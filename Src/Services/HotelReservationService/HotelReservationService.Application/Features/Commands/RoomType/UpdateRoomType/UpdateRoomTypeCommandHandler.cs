using AutoMapper;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.RoomType.UpdateRoomType;

public class UpdateRoomTypeCommandHandler:AsyncRequestHandler<UpdateRoomTypeCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IMapper _mapper;

    public UpdateRoomTypeCommandHandler(IUnitOfWork unitOfWork, IRoomTypeRepository roomTypeRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _roomTypeRepository = roomTypeRepository;
        _mapper = mapper;
    }
    protected override async Task Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var roomtype = _mapper.Map<UpdateRoomTypeCommand, Domain.Models.RoomType>(request);

        _roomTypeRepository.Update(roomtype);
        await _unitOfWork.SaveChangesAsync();
    }
}