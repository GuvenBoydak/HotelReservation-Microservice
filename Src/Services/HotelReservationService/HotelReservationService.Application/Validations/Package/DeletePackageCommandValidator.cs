using FluentValidation;
using HotelReservationService.Application.Features.Commands.Package.DeletePackage;

namespace HotelReservationService.Application.Validations.Package;

public class DeletePackageCommandValidator:AbstractValidator<DeletePackageCommand>
{
    public DeletePackageCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Field must not be empty");
    } 
}