using FluentValidation;
using HotelReservationService.Application.Features.Commands.RoomType.DeleteRoomType;

namespace HotelReservationService.Application.Validations.RoomType;

public class DeleteRoomTypeCommandValidator: AbstractValidator<DeleteRoomTypeCommand>
{
    public DeleteRoomTypeCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id Field must not be empty");
    }  
}