using FluentValidation;
using HotelReservationService.Application.Features.Commands.Reservation.DeleteReservation;

namespace HotelReservationService.Application.Validations.Reservation;

public class DeleteReservationCommandValidator:AbstractValidator<DeleteReservationCommand>
{
    public DeleteReservationCommandValidator()
    {
        RuleFor(x=>x.Id).NotEmpty().WithMessage("Id Field must not be empty");
    }
}