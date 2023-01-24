using HotelReservationService.Application.DTOs.RoomTypeDto;
using HotelReservationService.Application.Features.Commands.RoomType.CreateRoomType;
using HotelReservationService.Application.Features.Commands.RoomType.DeleteRoomType;
using HotelReservationService.Application.Features.Commands.RoomType.UpdateRoomType;
using HotelReservationService.Application.Features.Queries.Package.GetByIdPackageQuery;
using HotelReservationService.Application.Features.Queries.RoomType.GetAllRoomType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomTypesController:ControllerBase
{

    private readonly IMediator _mediator;

    public RoomTypesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll(GetAllRoomTypeQuery request)
    {
        return Ok(await _mediator.Send(request));
    }
    
    [HttpGet]
    [Route("{Id:guid}")]
    public async Task<IActionResult> GetById([FromRoute]GetByIdPackageQuery request)
    {
        return Ok(await _mediator.Send(request));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreateRoomTypeCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
    
    [HttpPut]
    public async Task<IActionResult> Update([FromBody]UpdateRoomTypeCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
    
    [HttpDelete]
    [Route("{Id:guid}")]
    public async Task<IActionResult> Delete([FromRoute]DeleteRoomTypeCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}