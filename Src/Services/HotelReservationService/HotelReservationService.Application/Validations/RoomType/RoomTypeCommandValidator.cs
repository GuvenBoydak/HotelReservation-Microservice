using System.Runtime.Intrinsics.X86;
using FluentValidation;
using HotelReservationService.Application.Features.Commands.RoomType.CreateRoomType;

namespace HotelReservationService.Application.Validations.RoomType;

public class CreateRoomTypeCommandValidator: AbstractValidator<CreateRoomTypeCommand>
{
    public CreateRoomTypeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Field must not be empty.");
        RuleFor(x => x.IsDoubleRoom).NotEmpty().WithMessage("IsDoubleRoom Field must not be empty.");
        RuleFor(x => x.Quantity).NotEmpty().WithMessage("Quantity Field must not be empty.");
        RuleFor(x => x.Price).NotEmpty().WithMessage("Price Field must not be empty.");
    }  
}