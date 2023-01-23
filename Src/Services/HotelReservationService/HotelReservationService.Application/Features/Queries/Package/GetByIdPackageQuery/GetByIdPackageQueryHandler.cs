using AutoMapper;
using HotelReservationService.Application.DTOs.PackageDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;

namespace HotelReservationService.Application.Features.Queries.Package.GetByIdPackageQuery;

public class GetByIdPackageQueryHandler : IRequestHandler<GetByIdPackageQuery, PackageDto>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IMapper _mapper;

    public GetByIdPackageQueryHandler(IPackageRepository packageRepository, IMapper mapper)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
    }

    public async Task<PackageDto> Handle(GetByIdPackageQuery request, CancellationToken cancellationToken)
    {
        var package = await _packageRepository.GetById(request.Id);
        
        if (package == null)
            throw new Exception("Package Not Found.");

        return _mapper.Map<Domain.Models.Package, PackageDto>(package);
    }
}