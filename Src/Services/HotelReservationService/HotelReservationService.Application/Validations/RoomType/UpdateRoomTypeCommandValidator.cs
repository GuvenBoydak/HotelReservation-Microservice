using FluentValidation;
using HotelReservationService.Application.Features.Commands.RoomType.UpdateRoomType;

namespace HotelReservationService.Application.Validations.RoomType;

public class UpdateRoomTypeCommandValidator: AbstractValidator<UpdateRoomTypeCommand>
{
    public UpdateRoomTypeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Field must not be empty");
    }  
}