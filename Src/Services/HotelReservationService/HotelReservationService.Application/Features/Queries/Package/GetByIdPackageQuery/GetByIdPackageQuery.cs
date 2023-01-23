using HotelReservationService.Application.DTOs.PackageDto;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Package.GetByIdPackageQuery;

public class GetByIdPackageQuery : IRequest<PackageDto>
{
    public Guid Id { get; set; }
}