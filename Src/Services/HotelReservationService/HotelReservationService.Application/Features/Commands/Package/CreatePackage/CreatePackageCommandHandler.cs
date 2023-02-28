using AutoMapper;
using HotelReservationService.Application.DTOs.PackageDto;
using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.Package.CreatePackage;

public class CreatePackageCommandHandler : IRequestHandler<CreatePackageCommand, PackageDto>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePackageCommandHandler(IPackageRepository packageRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _packageRepository = packageRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PackageDto> Handle(CreatePackageCommand request, CancellationToken cancellationToken)
    {
        var package = _mapper.Map<CreatePackageCommand, Domain.Models.Package>(request);

        var result = await _packageRepository.AddAsync(package);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<Domain.Models.Package, PackageDto>(result);
    }
}