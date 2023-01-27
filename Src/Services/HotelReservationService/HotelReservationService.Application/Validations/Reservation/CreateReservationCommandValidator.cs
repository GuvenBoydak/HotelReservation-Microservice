using FluentValidation;
using HotelReservationService.Application.Features.Commands.Reservation.CreateReservation;

namespace HotelReservationService.Application.Validations.Reservation;

public class CreateReservationCommandValidator:AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        RuleFor(x=>x.ReservationEmail).EmailAddress().WithMessage("The entered value must be in Email format.");
        RuleFor(x=>x.PackageId).NotEmpty().WithMessage("Package Field must not be empty");
        RuleFor(x=>x.RoomTypeId).NotEmpty().WithMessage("RoomType Field must not be empty");
        RuleFor(x=>x.ArrivalDate).NotEmpty().WithMessage("ArrivalDate Field must not be empty");
        RuleFor(x=>x.DepartureDate).NotEmpty().WithMessage("DepartureDate Field must not be empty");
        RuleFor(x=>x.Pax).NotEmpty().WithMessage("Pax Field must not be empty");
    }
}