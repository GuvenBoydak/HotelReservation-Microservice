using HotelReservationService.Application.DTOs.RoomTypeDto;
using HotelReservationService.Application.Features.Commands.RoomType.CreateRoomType;
using HotelReservationService.Application.Features.Commands.RoomType.DeleteRoomType;
using HotelReservationService.Application.Features.Commands.RoomType.UpdateRoomType;
using HotelReservationService.Application.Features.Queries.RoomType.GetAllRoomType;
using HotelReservationService.Application.Features.Queries.RoomType.GetByIdRoomType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseController;
using Shared.ResponceDto;

namespace HotelReservationService.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RoomTypesController : BaseController
{
    private readonly IMediator _mediator;

    public RoomTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllRoomTypeQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<List<RoomTypeListDto>>.Success(200, result));
    }

    [HttpGet]
    [Route("{Id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdRoomTypeQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<RoomTypeDto>.Success(200, result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoomTypeCommand request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<RoomTypeDto>.Success(200, result));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRoomTypeCommand request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<RoomTypeDto>.Success(200, result));
    }

    [HttpDelete]
    [Route("{Id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] DeleteRoomTypeCommand request)
    {
        await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}