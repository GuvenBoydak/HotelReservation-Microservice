using FakePaymentService.Application.DTOs;
using FakePaymentService.Application.Features.Queries.GetByCardNumber;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.BaseController;
using Shared.ResponceDto;

namespace FakePaymentService.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditCardsController : BaseController
{
    private readonly IMediator _mediator;

    public CreditCardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpGet("{CardNumber}")]
    public async Task<IActionResult> Get([FromRoute] GetByCardNumberQuery request)
    {
        var result = await _mediator.Send(request);
        return CreateActionResult(CustomResponseDto<CreditCardResponce>.Success(200, result));
    }
}