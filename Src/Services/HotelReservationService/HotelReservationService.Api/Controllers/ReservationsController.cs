using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.Features.Commands.Package.CreatePackage;
using HotelReservationService.Application.Features.Commands.Package.DeletePackage;
using HotelReservationService.Application.Features.Commands.Package.UpdatePackage;
using HotelReservationService.Application.Features.Commands.Reservation.CreateReservation;
using HotelReservationService.Application.Features.Commands.Reservation.DeleteReservation;
using HotelReservationService.Application.Features.Queries.Package.GetAllPackageQuery;
using HotelReservationService.Application.Features.Queries.Package.GetByIdPackageQuery;
using HotelReservationService.Application.Features.Queries.Reservation.GetAllReservation;
using HotelReservationService.Application.Features.Queries.Reservation.GetByConfirmationNumberReservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery]GetAllReservationQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    [Route("{Id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] GetAllReservationQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    [Route("{Id:guid}/Confirmation-number")]
    public async Task<IActionResult> GetByConfirmationNumber(
        [FromRoute] GetByConfirmationNumberReservationQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost]
    public async Task<ActionResult<CreateReservationDtoResponce>> Create([FromBody] CreateReservationCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] DeleteReservationCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}