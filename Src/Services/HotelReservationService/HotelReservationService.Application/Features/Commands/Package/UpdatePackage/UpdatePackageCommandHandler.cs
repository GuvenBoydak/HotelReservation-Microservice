using AutoMapper;
using HotelReservationService.Application.DTOs.PackageDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.Package.UpdatePackage;

public class UpdatePackageCommandHandler : IRequestHandler<UpdatePackageCommand, PackageDto>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePackageCommandHandler(IPackageRepository packageRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _packageRepository = packageRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<PackageDto> Handle(UpdatePackageCommand request, CancellationToken cancellationToken)
    {
        var package = _mapper.Map<UpdatePackageCommand, Domain.Models.Package>(request);

        var result = _packageRepository.Update(package);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<Domain.Models.Package, PackageDto>(result);
    }
}