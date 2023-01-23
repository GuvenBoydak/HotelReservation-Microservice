using FluentValidation;
using HotelReservationService.Application.Features.Commands.Package.CreatePackage;

namespace HotelReservationService.Application.Validations.Package;

public class CreatePackageCommandValidator:AbstractValidator<CreatePackageCommand>
{
    public CreatePackageCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Field must not be empty");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price Field must not be empty");
    } 
}