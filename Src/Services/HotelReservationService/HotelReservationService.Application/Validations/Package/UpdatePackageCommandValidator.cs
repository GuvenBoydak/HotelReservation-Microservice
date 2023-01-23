using FluentValidation;
using HotelReservationService.Application.Features.Commands.Package.UpdatePackage;

namespace HotelReservationService.Application.Validations.Package;

public class UpdatePackageCommandValidator:AbstractValidator<UpdatePackageCommand>
{
    public UpdatePackageCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Field must not be empty");
    } 
}