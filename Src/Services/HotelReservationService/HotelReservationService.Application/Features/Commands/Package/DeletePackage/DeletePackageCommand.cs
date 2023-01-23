using MediatR;

namespace HotelReservationService.Application.Features.Commands.Package.DeletePackage;

public class DeletePackageCommand : IRequest
{
    public Guid Id { get; set; }
}