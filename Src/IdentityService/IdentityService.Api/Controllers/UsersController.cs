using Bank.Application.DTOs.Token;
using Bank.Application.Features.Commands.Users.DeleteUser;
using Bank.Application.Features.Commands.Users.LoginUser;
using Bank.Application.Features.Commands.Users.RegisterUser;
using Bank.Application.Features.Commands.Users.UpdateUser;
using Bank.Application.Features.Queries.Users.GetAllUsers;
using Bank.Application.Features.Queries.Users.GetByIdUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<List<UserListDto>>> GetAllUser([FromQuery] GetAllUsersQuery request)
    {
        return Ok(await _mediator.Send(request));
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{Id:guid}")]
    public async Task<ActionResult> DeleteUser([FromRoute] DeleteUserCommand request)
    {
        return Ok(await _mediator.Send(request));
    }
}