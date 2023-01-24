using AutoMapper;
using HotelReservationService.Application.DTOs.PackageDto;
using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.DTOs.RoomTypeDto;
using HotelReservationService.Domain.Models;

namespace HotelReservationService.Application.Mapper;

public class MapProfile:Profile
{
    public MapProfile()
    {
        CreateMap<Reservation, ReservationDto>().ReverseMap();
        CreateMap<Reservation, ReservationListDto>().ReverseMap();
        
        CreateMap<RoomType, RoomTypeDto>().ReverseMap();
        CreateMap<RoomType, RoomTypeListDto>().ReverseMap();
        
        CreateMap<Package, PackageDto>().ReverseMap();
        CreateMap<Package, PackageListDto>().ReverseMap();
    }
}