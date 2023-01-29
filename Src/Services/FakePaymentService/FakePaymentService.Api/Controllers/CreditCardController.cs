using FakePaymentService.Application.Features.Queries.GetByCardNumber;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FakePaymentService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditCardController : ControllerBase
{
    private readonly IMediator _mediator;

    public CreditCardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] GetByCardNumberQuery request)
    {
        return Ok(await _mediator.Send(request));
    }
}
