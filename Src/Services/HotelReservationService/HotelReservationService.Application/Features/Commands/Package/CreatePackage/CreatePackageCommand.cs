using HotelReservationService.Application.DTOs.PackageDto;
using MediatR;

namespace HotelReservationService.Application.Features.Commands.Package.CreatePackage;

public class CreatePackageCommand:IRequest<PackageDto>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}