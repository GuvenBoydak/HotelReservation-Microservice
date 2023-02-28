using IdentityService.Application.Dtos;
using IdentityService.Application.Features.Commands.Users.DeleteUser;
using IdentityService.Application.Features.Commands.Users.LoginUser;
using IdentityService.Application.Features.Commands.Users.RegisterUser;
using IdentityService.Application.Features.Commands.Users.UpdateUser;
using IdentityService.Application.Features.Queries.GetAllUsers;
using IdentityService.Application.Features.Queries.GetByIdUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseController;
using Shared.ResponceDto;

namespace IdentityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUser([FromQuery] GetAllUsersQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<List<UserListDto>>.Success(200, result));
    }

    // [Authorize(Roles = "Admin")]
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetByIdUser([FromRoute] GetByIdUserQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<UserDto>.Success(200, result));
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserCommand request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<UserDto>.Success(200, result));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserCommand request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<AccessToken>.Success(200, result));
    }

    // [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<UserDto>.Success(200, result));
    }

    // [Authorize(Roles = "Admin")]
    [HttpDelete("{Id:guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] DeleteUserCommand request)
    {
        await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
    }
}