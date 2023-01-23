using AutoMapper;
using HotelReservationService.Application.DTOs.RoomTypeDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.RoomType.GetByIdRoomType;

public class GetByIdRoomTypeQueryHandler:IRequestHandler<GetByIdRoomTypeQuery,RoomTypeDto>
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IMapper _mapper;

    public GetByIdRoomTypeQueryHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper)
    {
        _roomTypeRepository = roomTypeRepository;
        _mapper = mapper;
    }

    public async Task<RoomTypeDto> Handle(GetByIdRoomTypeQuery request, CancellationToken cancellationToken)
    {
        var roomType = await _roomTypeRepository.GetById(request.Id);

        if (roomType == null)
            throw new Exception("RoomType Not Found");

        return _mapper.Map<Domain.Models.RoomType, RoomTypeDto>(roomType);
    }
}