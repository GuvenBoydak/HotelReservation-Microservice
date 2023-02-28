using HotelReservationService.Application.DTOs.PackageDto;
using HotelReservationService.Application.Features.Commands.Package.CreatePackage;
using HotelReservationService.Application.Features.Commands.Package.DeletePackage;
using HotelReservationService.Application.Features.Commands.Package.UpdatePackage;
using HotelReservationService.Application.Features.Queries.Package.GetAllPackageQuery;
using HotelReservationService.Application.Features.Queries.Package.GetByIdPackageQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseController;
using Shared.ResponceDto;

namespace HotelReservationService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class PackagesController : BaseController
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
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<List<PackageListDto>>.Success(200, result));
    }

    [HttpGet]
    [Route("{Id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdPackageQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<PackageDto>.Success(200, result));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePackageCommand request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<PackageDto>.Success(200, result));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePackageCommand request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<PackageDto>.Success(200, result));
    }

    [HttpDelete]
    [Route("{Id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] DeletePackageCommand request)
    {
        await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}