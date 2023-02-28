using AutoMapper;
using HotelReservationService.Application.DTOs.RoomTypeDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.RoomType.CreateRoomType;

public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, RoomTypeDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IMapper _mapper;

    public CreateRoomTypeCommandHandler(IUnitOfWork unitOfWork, IRoomTypeRepository roomTypeRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _roomTypeRepository = roomTypeRepository;
        _mapper = mapper;
    }

    public async Task<RoomTypeDto> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var roomType = _mapper.Map<CreateRoomTypeCommand, Domain.Models.RoomType>(request);

        var result = await _roomTypeRepository.AddAsync(roomType);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<Domain.Models.RoomType, RoomTypeDto>(result);
    }
}