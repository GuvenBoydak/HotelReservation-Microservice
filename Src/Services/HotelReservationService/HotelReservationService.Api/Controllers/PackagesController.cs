using HotelReservationService.Application.Features.Commands.Package.CreatePackage;
using HotelReservationService.Application.Features.Commands.Package.DeletePackage;
using HotelReservationService.Application.Features.Commands.Package.UpdatePackage;
using HotelReservationService.Application.Features.Queries.Package.GetAllPackageQuery;
using HotelReservationService.Application.Features.Queries.Package.GetByIdPackageQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PackagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PackagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllPackageQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    [Route("{Id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdPackageQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePackageCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePackageCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpDelete]
    [Route("{Id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] DeletePackageCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}