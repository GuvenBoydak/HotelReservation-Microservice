using HotelReservationService.Application.DTOs.ReservationDto;
using HotelReservationService.Application.Features.Commands.Reservation.CreateReservation;
using HotelReservationService.Application.Features.Commands.Reservation.DeleteReservation;
using HotelReservationService.Application.Features.Queries.Reservation.GetAllReservation;
using HotelReservationService.Application.Features.Queries.Reservation.GetByConfirmationNumberReservation;
using HotelReservationService.Application.Features.Queries.Reservation.GetByIdReservation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseController;
using Shared.ResponceDto;

namespace HotelReservationService.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReservationController : BaseController
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllReservationQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<List<ReservationListDto>>.Success(200, result));
    }

    [HttpGet]
    [Route("{Id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdReservationQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<ReservationDto>.Success(200, result));
    }

    [HttpGet]
    [Route("{Id:guid}/Confirmation-number")]
    public async Task<IActionResult> GetByConfirmationNumber(
        [FromRoute] GetByConfirmationNumberReservationQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<ReservationDto>.Success(200, result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationCommand request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<CreateReservationDtoResponce>.Success(200, result));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] DeleteReservationCommand request)
    {
        await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<CreateReservationDtoResponce>.Success(204));
    }
}