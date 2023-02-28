using AutoMapper;
using HotelReservationService.Application.DTOs.RoomTypeDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.RoomType.UpdateRoomType;

public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, RoomTypeDto>
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

    public async Task<RoomTypeDto> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var roomtype = _mapper.Map<UpdateRoomTypeCommand, Domain.Models.RoomType>(request);

        var result = _roomTypeRepository.Update(roomtype);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<Domain.Models.RoomType, RoomTypeDto>(result);
    }
}