using AutoMapper;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.RoomType.CreateRoomType;

public class CreateRoomTypeCommandHandler:AsyncRequestHandler<CreateRoomTypeCommand>
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

    protected override async Task Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
    {
        var roomType = _mapper.Map<CreateRoomTypeCommand, Domain.Models.RoomType>(request);

        await _roomTypeRepository.AddAsync(roomType);
        await _unitOfWork.SaveChangesAsync();
    }
}