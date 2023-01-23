using HotelReservationService.Application.DTOs.PackageDto;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Package.GetAllPackageQuery;

public class GetAllPackageQuery : IRequest<List<PackageListDto>>
{
}