using HotelReservationService.Application.İnterfaces.Repositories;
using MediatR;
using Shared.Infrastructure.EntityFramework;

namespace HotelReservationService.Application.Features.Commands.Package.DeletePackage;

public class DeletePackageCommandHandler : AsyncRequestHandler<DeletePackageCommand>
{
    private readonly IPackageRepository _packageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeletePackageCommandHandler(IPackageRepository packageRepository, IUnitOfWork unitOfWork)
    {
        _packageRepository = packageRepository;
        _unitOfWork = unitOfWork;
    }

    protected override async Task Handle(DeletePackageCommand request, CancellationToken cancellationToken)
    {
        await _packageRepository.DeleteAsync(request.Id);
        await _unitOfWork.SaveChangesAsync();
    }
}