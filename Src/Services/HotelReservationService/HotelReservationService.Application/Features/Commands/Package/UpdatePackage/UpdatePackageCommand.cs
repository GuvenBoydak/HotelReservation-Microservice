using MediatR;

namespace HotelReservationService.Application.Features.Commands.Package.UpdatePackage;

public class UpdatePackageCommand:IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}