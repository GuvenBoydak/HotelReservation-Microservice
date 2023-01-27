using AutoMapper;
using HotelReservationService.Application.DTOs.PackageDto;
using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.DTOs.RoomTypeDto;
using HotelReservationService.Application.Features.Commands.Package.CreatePackage;
using HotelReservationService.Application.Features.Commands.Package.UpdatePackage;
using HotelReservationService.Application.Features.Commands.Reservation.CreateReservation;
using HotelReservationService.Application.Features.Commands.RoomType.UpdateRoomType;
using HotelReservationService.Domain.Models;

namespace HotelReservationService.Application.Mapper;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<Reservation, ReservationDto>().ReverseMap();
        CreateMap<Reservation, ReservationListDto>().ReverseMap();
        CreateMap<Reservation, CreateReservationCommand>().ReverseMap();
        
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeListDto>().ReverseMap();
        CreateMap<RoomType, CreatePackageCommand>().ReverseMap();
        CreateMap<RoomType, UpdateRoomTypeCommand>().ReverseMap();
        
        CreateMap<Package, PackageDto>().ReverseMap();
        CreateMap<Package, PackageListDto>().ReverseMap();
        CreateMap<Package, CreatePackageCommand>().ReverseMap();
        CreateMap<Package, UpdatePackageCommand>().ReverseMap();
    }
}