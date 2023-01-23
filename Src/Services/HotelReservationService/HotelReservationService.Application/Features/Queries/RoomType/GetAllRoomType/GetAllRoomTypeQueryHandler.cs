using AutoMapper;
using HotelReservationService.Application.DTOs.RoomTypeDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.RoomType.GetAllRoomType;

public class GetAllRoomTypeQueryHandler : IRequestHandler<GetAllRoomTypeQuery, List<RoomTypeListDto>>
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IMapper _mapper;

    public GetAllRoomTypeQueryHandler(IRoomTypeRepository roomTypeRepository, IMapper mapper)
    {
        _roomTypeRepository = roomTypeRepository;
        _mapper = mapper;
    }

    public async Task<List<RoomTypeListDto>> Handle(GetAllRoomTypeQuery request, CancellationToken cancellationToken)
    {
        var roomType = await _roomTypeRepository.GetAllAsync();

        return _mapper.Map<List<Domain.Models.RoomType>, List<RoomTypeListDto>>(roomType);
    }
}