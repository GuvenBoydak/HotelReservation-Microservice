using AutoMapper;
using HotelReservationService.Application.DTOs.PackageDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Package.GetAllPackageQuery;

public class GetAllPackageQueryHandler : IRequestHandler<GetAllPackageQuery, List<PackageListDto>>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IMapper _mapper;

    public GetAllPackageQueryHandler(IPackageRepository packageRepository, IMapper mapper)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
    }

    public async Task<List<PackageListDto>> Handle(GetAllPackageQuery request, CancellationToken cancellationToken)
    {
        var packages = await _packageRepository.GetAllAsync();
        return _mapper.Map<List<Domain.Models.Package>, List<PackageListDto>>(packages);
    }
}