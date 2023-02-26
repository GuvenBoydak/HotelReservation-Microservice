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

namespace IdentityService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<UserListDto>>> GetAllUser([FromQuery] GetAllUsersQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

   // [Authorize(Roles = "Admin")]
    [HttpGet("{Id}")]
    public async Task<ActionResult<UserDto>> GetByIdUser([FromRoute] GetByIdUserQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AccessToken>> LoginUser([FromBody] LoginUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

   // [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

   // [Authorize(Roles = "Admin")]
    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult> DeleteUser([FromRoute] DeleteUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}